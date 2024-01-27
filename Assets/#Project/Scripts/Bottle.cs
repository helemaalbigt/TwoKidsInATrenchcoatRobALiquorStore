using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {
    public float price;
    public string bottleName;
    
    [Space(15)]
    public OVRGrabbable grabbable;
    public Collider collider;
}
