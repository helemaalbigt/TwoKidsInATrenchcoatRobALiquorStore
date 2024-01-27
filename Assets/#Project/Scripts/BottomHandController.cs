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
    
    private Vector3 _targetHandPos;
    
    private void Update() {
        PositionHand();
    }

    private void PositionHand() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, hittableHandLayers)) {
            if (grabber.grabbedObject != null) {
                _targetHandPos = hit.point - ray.direction.normalized * 0.04f;
            } else {
                _targetHandPos = hit.point;
            }
        }

        hand.position = Vector3.Slerp(hand.position, _targetHandPos, Time.unscaledDeltaTime * 8f);
    }
}
