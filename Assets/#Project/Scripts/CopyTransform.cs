using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour {
    public Transform target;

    private void Update() {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
