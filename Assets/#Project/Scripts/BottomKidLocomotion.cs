using System.Linq;
using UnityEngine;

public class BottomKidLocomotion : MonoBehaviour {
    public float speed;
    public float rotationSpeed;
    
    [Space(15)]
    public Rigidbody rigidBody;
    public Transform head;

    private bool _prevPosSet;
    private Vector3 _prevPos;
    
    void FixedUpdate() {
        Move();
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
}
