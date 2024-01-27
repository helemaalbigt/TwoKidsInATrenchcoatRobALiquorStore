using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHandController : MonoBehaviour
{
    public LayerMask hittableHandLayers;
    public LayerMask snappableLayers;
    
    public Camera camera;
    public Transform hand;
    public MouseGrabber grabber;
    public float offsetNoGrab = 0.01f;
    public float offsetGrab = 0.04f;
    
    private Vector3 _targetHandPos;
    private bool _grabbedThisFrame;
    private bool _grabbedLastFrame;

    private Transform _grabbedObject;
    private Transform _snapAnchor;
    
    private void Update() {
        _grabbedThisFrame = grabber.grabbedObject;
        PositionHand();
        
        if (_grabbedThisFrame && !_grabbedLastFrame) {
            //_grabbedObject = grabber.
        }

        if (_grabbedThisFrame) {
            CheckForSnapAnchor();
        }

        if (!_grabbedThisFrame && _grabbedLastFrame) {
            CheckToPlaceObject();
        }
        
        _grabbedLastFrame = _grabbedThisFrame;
    }

    private void PositionHand() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, hittableHandLayers)) {
            var offset = grabber.grabbedObject ? offsetGrab : offsetNoGrab;
            _targetHandPos = hit.point - ray.direction.normalized * offset;
        }

        hand.position = Vector3.Slerp(hand.position, _targetHandPos, Time.unscaledDeltaTime * 8f);
    }

    private void CheckForSnapAnchor() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, snappableLayers)) {
            _snapAnchor = hit.transform;
        } else {
            _snapAnchor = null;
        }
    }

    private void CheckToPlaceObject() {
        
    }
}
