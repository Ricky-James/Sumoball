using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerCCMovement : MonoBehaviour
{

    public CharacterController controller;

    [Range(1,15)]
    public float speed;
    private const float maxSpeed = 15;
    private const float acceleration = 1.05f;
    private const float decelleration = 0.9f;
    private const float gravity = -9.81f;
    Vector3 velocity;

    public Transform groundCheck;
    private const float groundDistance = 0.4f; //Radius of ground check
    public LayerMask groundMask;
    private bool isGrounded;

    private void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xInput + transform.forward * zInput;
        controller.Move(move.normalized * speed * Time.deltaTime);

        //Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -1f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        
    }


}
