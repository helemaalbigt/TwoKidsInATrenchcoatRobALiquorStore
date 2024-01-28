using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public float speed = 20f;
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.unscaledDeltaTime * speed);
    }
}
