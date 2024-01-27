using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHandController : MonoBehaviour
{
    public LayerMask hittableHandLayers;
    public Camera camera;
    public Transform hand;
    public MouseGrabber grabber;
    public float offsetNoGrab = 0.01f;
    public float offsetGrab = 0.04f;
    
    private Vector3 _targetHandPos;
    
    private void Update() {
        PositionHand();
    }

    private void PositionHand() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, hittableHandLayers)) {
            var offset = grabber.grabbedObject ? offsetGrab : offsetNoGrab;
            _targetHandPos = hit.point - ray.direction.normalized * offset;
        }

        hand.position = Vector3.Slerp(hand.position, _targetHandPos, Time.unscaledDeltaTime * 8f);
    }
}
