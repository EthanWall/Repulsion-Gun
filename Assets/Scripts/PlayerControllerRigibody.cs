using UnityEngine;
using System.Collections;
 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class PlayerControllerRigibody : MonoBehaviour {
 
    new private Rigidbody rigidbody;
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
 
 
 
	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
	    rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;

        Cursor.lockState = CursorLockMode.Locked;
	}
 
	void FixedUpdate () {
	    if (grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
 
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
	        }
	    }
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0.0f, -gravity * rigidbody.mass, 0.0f));
 
	    grounded = false;

        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
        }
	}
 
	void OnCollisionStay () {
	    grounded = true;    
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2.0f * jumpHeight * gravity);
	}
}