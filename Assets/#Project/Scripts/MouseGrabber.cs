using UnityEngine;
using UnityEngine.Events;

public class MouseGrabber : OVRGrabber
{

    public UnityEvent OnGrabStarted;
    public UnityEvent OnGrabEnded;
    public UnityEvent OnGrabObjectStarted;

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            GrabBegin();
        }

        if (Input.GetMouseButtonUp(0))
        {
            GrabEnd();
        }
    }

    protected override void GrabBegin()
    {
        base.GrabBegin();

        OnGrabStarted.Invoke();
        if (grabbedObject != null)
            OnGrabObjectStarted.Invoke();
    }

    protected override void GrabVolumeEnable(bool enabled)
    {
        base.GrabVolumeEnable(enabled);

        OnGrabEnded.Invoke();
    }
}
