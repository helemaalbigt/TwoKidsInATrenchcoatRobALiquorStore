using UnityEngine;

public class MouseGrabber : OVRGrabber
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            GrabBegin();
        }
        
        if (Input.GetMouseButtonUp(0)) {
            GrabEnd();
        }
    }
}
