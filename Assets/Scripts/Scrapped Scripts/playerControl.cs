using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class playerControl : MonoBehaviour
{
    public InputManager input;
   // [Range(1, 30)]
    public float moveSpeed;

    private float vertical;
    private float horizontal;

    public const float rotationSpeed = 125;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;

    private void Start()
    {
        input = GetComponent<InputManager>(); //Safety check in case it's not set in inspector
    }


    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        CheckInput();
    }

    private void CheckInput()
    {
  
        //float rotation = (transform.rotation * rb.velocity).x;

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, (moveSpeed * vertical) * Time.deltaTime);


        transform.Rotate((transform.up * horizontal) * rotationSpeed * Time.deltaTime);



       // rb.AddForce((transform.forward * vertical) * moveSpeed * Time.deltaTime);

       // transform.Rotate(new Vector3(0, horizontalRotation, 0));
    }


}
