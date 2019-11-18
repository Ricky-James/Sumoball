using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [Range(1, 30)]
    public float moveSpeed;

    private float vertical;
    private float horizontal;

    private const float rotationSpeed = 125;

    [SerializeField] private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        CheckInput();
    }

    private void CheckInput()
    {

        // https://forum.unity.com/threads/proper-velocity-based-movement-101.462598/
        rb.velocity = (transform.forward * vertical) * moveSpeed;
        transform.Rotate((transform.up * horizontal) * rotationSpeed * Time.deltaTime);

       // transform.Rotate(new Vector3(0, horizontalRotation, 0));
    }


}
