using UnityEngine;

public class MouseGrabber : OVRGrabber
{
    public override void Update()
    {
        base.Update();
        
        if (Input.GetMouseButtonDown(0)) {
            GrabBegin();
        }
        
        if (Input.GetMouseButtonUp(0)) {
            GrabEnd();
        }
    }
}
