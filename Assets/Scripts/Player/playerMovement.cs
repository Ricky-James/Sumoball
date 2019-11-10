using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))] //Player input

public class playerMovement : MonoBehaviour
{
    public InputManager input;
    public Rigidbody rb;
    public Transform cam; //Cam target + direction
    
    // Start is called before the first frame update
    void Start()
    {
        if(input == null) input = GetComponent<InputManager>();
        if(rb == null) rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
       


        rb.AddForce(cam.forward * 15 * input.throttle());
        rb.AddForce(cam.right * 15 * input.direction());


        ///OLD SCRIPT please ignore.///
        ///////////////////////////////

        //this.transform.LookAt(target);
        //this.transform.position = rb.position;

        //if(input.throttle() > 0)
        //{
        //    if (speed < maxSpeed)
        //    {
        //        speed += (Time.deltaTime * 5f);
        //    }
        //    else{
        //        speed = maxSpeed;
        //    }

        //} else if (input.throttle() < 0) //Reverse
        //{
        //    if (speed > -maxSpeed)
        //    {
        //        speed -= (Time.deltaTime * 5f);
        //    }
        //    else
        //    {
        //        speed = -maxSpeed;
        //    }
        //} else if (input.throttle() == 0)
        //{
        //    speed *= 0.95f;
        //}
        //Debug.Log(speed);

        
        //rb.AddForce(Vector3.forward * speed * input.throttle());

        

    }
}
