using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public OVRGrabbable grabbable;
    public Collider collider;
    
    void Update() {
        collider.enabled = !grabbable.isGrabbed;
    }
}
