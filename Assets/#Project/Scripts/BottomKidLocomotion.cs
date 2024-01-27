using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BottomKidLocomotion : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
        
    public float lookSensitivity = 10f;
    public float maxYAngle = 80f;
    public float maxXAngle = 80f;
    public LayerMask hittableHandLayers;
    
    [Space(15)]
    public Rigidbody rigidBody;
    public Transform head;
    public Camera camera;
    public Transform hand;
    public MouseGrabber grabber;
    
    private Vector3 _targetHandPos;
    private Vector2 _currentRotation;
    
    void FixedUpdate() {
        Move();
        Look();
        PositionHand();
    }

    private void Move() {
        Vector3 movement = Vector3.zero;
        Vector3 angularVel = Vector3.zero;
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            movement += transform.forward * speed;
        }
        
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            movement -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            angularVel += new Vector3(0, -rotationSpeed, 0);
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            angularVel += new Vector3(0, rotationSpeed, 0);
        }

        rigidBody.velocity = movement;
        rigidBody.angularVelocity = angularVel;
    }

    private void Look() {
        _currentRotation.x += Input.GetAxis("Mouse X") * lookSensitivity;
        _currentRotation.y -= Input.GetAxis("Mouse Y") * lookSensitivity;
        _currentRotation.x = Mathf.Clamp(_currentRotation.x, -maxXAngle, maxXAngle);
        _currentRotation.y = Mathf.Clamp(_currentRotation.y, -maxYAngle, maxYAngle);
        
        head.localRotation = Quaternion.Euler(_currentRotation.y,_currentRotation.x,0);
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
