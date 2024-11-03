using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/*
 Implementation of the circuit completion puzzle.
 Player should rotate switches to make correct connection to deliver power from source to destination.
 Two switches are is "powered" when at least one of them is "powered" directly or indirectly to the source, 
 and both switches are facing same type of connector to each other (blue-blue, yellow, yellow)
 */
public class PuzzleGrid : MonoBehaviour
{

    private int height = 7;
    private int width = 7;
    private Tilemap wires, switches, terminals;
    [SerializeField] private Sprite sprite_wire_h, sprite_wire_v, sprite_switch, sprite_terminal;
    /*
     X for empty cell,
     - and | for horizontal and vertical connection,
     S and D for source and destination,
     integers for switches.
     Since validating connection through existence of wires is difficult,
     Identifying wires will be done by reading edge informations.
     */
    private string[] gridMap = {"XXXXXXX",
                                "S-----1",
                                "|XXXXX|",
                                "2-----D",
                                "|XXXXX|",
                                "3-----4",
                                "XXXXXXX"};
    /*
     Switches can be either fixed or rotatable.
     switchInfo[i] describes switch i in the gridmap, except for switchInfo[0] (=Source) and switchInfo[N-1] (=Destination).
     It consists with at least 5 characters.
     The first character tells whether the switch is fixed(1) or not(0).
     The next four characters denotes the connection with connector type, in top, right, bottom, left order.
     Each of the four digits are eigher "X"(no connection), "B"(blue connector), "Y"(yellow connector).
     The rest character denotes other switches it is adjacent to (might be powered if aligned correctly).
     Source and Destination will always be fixed for clarity.
     Make sure that the solution exists!
    */
    private string[] switchInfo = {"0XBBX12",
                                   "1BXXBSD",
                                   "1BBXBS3D",
                                   "0BBXX24",
                                   "1XBBX3D",
                                   "0BXBB124"};

    private Vector3 origin;

    void Awake()
    {
        Tilemap[] tilemaps = gameObject.GetComponentsInChildren<Tilemap>();
        wires = tilemaps[0];
        switches = tilemaps[1];
        terminals = tilemaps[2];

        origin = wires.WorldToCell(GameObject.Find("Pivot").transform.position);
        Debug.Log("origin: " + origin);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
