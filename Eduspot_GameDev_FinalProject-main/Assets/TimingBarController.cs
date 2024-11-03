using UnityEngine;

public class TimingBarController : MonoBehaviour
{
    public bool collidingTarget = false;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.transform.name == "TargetRange")
            collidingTarget = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.transform.name == "TargetRange")
            collidingTarget = false;
    }
}
