using UnityEngine;

public class BottomKidLocomotion : MonoBehaviour {
    public Rigidbody rigidBody;
    public float speed;
    public float rotationSpeed;
    
    void FixedUpdate() {
        Vector3 movement = Vector3.zero;
        Vector3 angularVel = Vector3.zero;
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            movement += transform.forward * speed;
        }
        
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            movement -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            angularVel += new Vector3(0, -rotationSpeed, 0);
        }
        
        if (Input.GetKey(KeyCode.RightArrow)) {
            angularVel += new Vector3(0, rotationSpeed, 0);
        }

        rigidBody.velocity = movement;
        rigidBody.angularVelocity = angularVel;
    }
}
