using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHeadRecenter : MonoBehaviour {
    public Transform cameraRig;
    public Transform head;
    public Transform headTarget;

    [Space(15)] public float maxDistFromCenter = 0.15f;

    void Update() {
        if (Vector3.Distance(head.position, headTarget.position) > maxDistFromCenter) {
            var diff = headTarget.position - head.position;
            cameraRig.position += diff;
        }
    }
}
