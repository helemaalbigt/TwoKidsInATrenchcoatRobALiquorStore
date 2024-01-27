using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomHandController : MonoBehaviour
{
    public LayerMask hittableHandLayers;
    public LayerMask hittableHandLayersNoInteractive;
    public LayerMask snappableLayers;
    
    public Camera camera;
    public Transform hand;
    public MouseGrabber grabber;
    public float offsetNoGrab = 0.01f;
    public float offsetGrab = 0.04f;
    
    private Vector3 _targetHandPos;
    private bool _grabbedThisFrame;
    private bool _grabbedLastFrame;

    [Header("Debug")]
    public Transform _grabbedObject;
    public Rigidbody _grabbedObjectRb;
    public Transform _snapAnchor;
    
    private void Update() {
        _grabbedThisFrame = grabber.grabbedObject;
        PositionHand();
        
        if (_grabbedThisFrame && !_grabbedLastFrame) {
            _grabbedObject = grabber.grabbedObject.transform;
            _grabbedObjectRb = _grabbedObject.GetComponent<Rigidbody>();
            _grabbedObjectRb.useGravity = false;
        }

        if (_grabbedThisFrame) {
            CheckForSnapAnchor();
        }

        if (!_grabbedThisFrame && _grabbedLastFrame) {
            HandleReleaseObject();
        }
        
        _grabbedLastFrame = _grabbedThisFrame;
    }

    private void PositionHand() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var layers = grabber.grabbedObject ? hittableHandLayersNoInteractive : hittableHandLayers;
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, layers)) {
            var offset = grabber.grabbedObject ? offsetGrab : offsetNoGrab;
            _targetHandPos = hit.point - ray.direction.normalized * offset;
        }

        if (_snapAnchor != null) {
            _targetHandPos = _snapAnchor.position;
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

    private void HandleReleaseObject() {
        if (_snapAnchor != null) {
            _grabbedObject.position = _snapAnchor.position;
            _grabbedObject.rotation = _snapAnchor.rotation;
            _grabbedObjectRb.isKinematic = true;
            _grabbedObjectRb.velocity = _grabbedObjectRb.angularVelocity = Vector3.zero;
            var colls = _grabbedObject.GetComponentsInChildren<Collider>();
            foreach (var coll in colls) {
                coll.enabled = false;
            }
        } else {
            _grabbedObjectRb.useGravity = true;
        }

        _snapAnchor = null;
        _grabbedObjectRb = null;
        _grabbedObject = null;
    }
}
