using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    CharacterController characterController;
    new private Collider collider;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private float vSpeed = 0.0f;

    private Vector3 moveDirection = Vector3.zero;

    private float distToGround;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        collider = GetComponent<Collider>();
        
        distToGround = collider.bounds.extents.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
        moveDirection *= speed;

        if (characterController.isGrounded) {
            vSpeed = 0;
            if (Input.GetButton("Jump")) {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;

        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown("escape")) {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
