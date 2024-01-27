using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomOVRGrabber : OVRGrabber
{
    public UnityEvent OnGrabStarted;
    public UnityEvent OnGrabEnded;

    protected override void GrabBegin()
    {
        base.GrabBegin();

        OnGrabStarted.Invoke();
    }

    protected override void GrabVolumeEnable(bool enabled)
    {
        base.GrabVolumeEnable(enabled);

        OnGrabEnded.Invoke();
    }
}
