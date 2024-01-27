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

    private bool _prevPosSet;
    private Vector3 _prevPos;

    private Vector2 currentRotation;
    
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
        currentRotation.x += Input.GetAxis("Mouse X") * lookSensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * lookSensitivity;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -maxXAngle, maxXAngle);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        
        head.localRotation = Quaternion.Euler(currentRotation.y,currentRotation.x,0);
    }

    private void PositionHand() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, hittableHandLayers)) {
            hand.position = hit.point;
        }
    }
}
