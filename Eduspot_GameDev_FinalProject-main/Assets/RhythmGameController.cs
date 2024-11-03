using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RhythmGameController : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject display;
    [SerializeField] GameObject timingBar;
    [SerializeField] GameObject progressDisplay;

    private GameObject targetRange;
    private SpriteRenderer[] progressDisplays;

    private float left, right;

    private float timingBarSpeed = 0.0f;
    private float[] timingBarSpeeds = {5.0f, 7.5f, 10.0f};
    private bool moving = true;
    private bool headingRight = true;
    private bool blinking = true;

    private float[] targetRangeScales = {0.16f, 0.12f, 0.08f};
    private int clearCount = 0;


    void Awake()
    {
        targetRange = display.transform.GetChild(0).gameObject;
        progressDisplays = progressDisplay.GetComponentsInChildren<SpriteRenderer>();
    }

    void Start() 
    {
        left = (display.transform.position - display.GetComponent<SpriteRenderer>().bounds.size / 2).x;
        right = (display.transform.position + display.GetComponent<SpriteRenderer>().bounds.size / 2).x;

        SpawnTargetRectangle(targetRangeScales[clearCount]);
        timingBarSpeed = timingBarSpeeds[clearCount];
        if (blinking) 
        {
            StartCoroutine(Blink());
        }
        
    }

    private void SpawnTargetRectangle(float scale_x)
    {
        targetRange.transform.localScale = new Vector3(scale_x, 
                                                    targetRange.transform.localScale.y,
                                                    targetRange.transform.localScale.z);

        float leftLimit = -0.5f + scale_x / 2;
        float rightLimit = 0.5f - scale_x / 2;

        float randomPosition = Random.Range(leftLimit, rightLimit);
        targetRange.transform.localPosition = new Vector3(randomPosition, 
                                                    targetRange.transform.localPosition.y,
                                                    targetRange.transform.localPosition.z);
    }

    void Update()
    {
        bool getInput = Input.GetKeyDown(KeyCode.Space);
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);
            bool rc  = Physics.Raycast(ray, out hit, 1000.0f);
            Debug.Log(rc);
            getInput = rc && hit.transform.name == "Button";
        }

        if (moving) {
            if (getInput)
            {
                bool success = CheckSuccess();
                if (success && clearCount < 2)
                {
                    progressDisplays[clearCount].color = Color.green;
                    clearCount += 1;
                    SpawnTargetRectangle(targetRangeScales[clearCount]);
                    timingBarSpeed = timingBarSpeeds[clearCount];
                }
                else if (success && clearCount >= 2)
                {
                    progressDisplays[clearCount].color = Color.green;
                    moving = false;
                    StopAllCoroutines();
                    ChangeColor(1.0f);
                }
            }

            float current_x = timingBar.transform.position.x;
            float movement = 0;
            if (headingRight) {
                movement = timingBarSpeed * Time.deltaTime;
                if (current_x > right) {
                    movement = right - current_x;
                    headingRight = !headingRight;
                }
            }
            else {
                movement = -timingBarSpeed * Time.deltaTime;
                if (current_x < left) {
                    movement = left - current_x;
                    headingRight = !headingRight;
                }
            }
            timingBar.transform.position += new Vector3(movement, 0, 0);
        }
        
    }

    private bool CheckSuccess() {
        return timingBar.GetComponent<TimingBarController>().collidingTarget;
    }

    private void ChangeColor(float alpha)
    {
        SpriteRenderer sr = timingBar.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        foreach (SpriteRenderer sr_child in timingBar.GetComponentsInChildren<SpriteRenderer>())
        {
            sr_child.color = new Color(sr_child.color.r, sr_child.color.g, sr_child.color.b, alpha);
        }
    }

    private IEnumerator Blink() 
    {
        float alpha = 1.0f;
        float blinkspeed = 0.001f;
        while (true)
        {
            if (alpha >= 1.0f)
            {
                while (alpha > 0.0f)
                {
                    alpha -= blinkspeed;
                    ChangeColor(alpha);
                    yield return null;
                }
            }
            if (alpha <= 0.0f)
            {
                yield return new WaitForSeconds(0.5f);
                while (alpha < 1.0f)
                {
                    alpha += blinkspeed;
                    ChangeColor(alpha);
                    yield return null;
                }
            }
        }
    }
}