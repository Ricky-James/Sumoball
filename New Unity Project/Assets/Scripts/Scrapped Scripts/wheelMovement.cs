//SCRIPT WRITTEN BY RICKY JAMES - Last updated 10/11/2019
//SCRAPPED SCRIPT SAVED FOR FUTURE REFERENCE
///Movement script dependant on wheel collider for turning
///Essentially attempts to function somewhat like a monocycle, only without the leaning
///Doesn't allow for rolling left/right and makes some collisions a little odd
///Wasn't able to appropriately implement the rotation of the model
///Collider rotation (wheel.SteerAngle) isn't easily utilised for actual rotation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))] //Dependancy to function, auto-attachs IM to any object with this script
public class wheelMovement : MonoBehaviour
{

    public InputManager input;
    public WheelCollider wheel;


    [Range(50, 5000)]
    [SerializeField] private int strength = 5000;

    public Transform pmodel;

    private float turn;
    [Range(1,2)]
    public float turnRate;

    public float getTurn()
    {
        return turn;
    }

    void Start()
    {
        input = GetComponent<InputManager>(); //Safety check in case it's not set in inspector
    }
    

    void FixedUpdate()
    {
        //Throttle

        wheel.motorTorque = strength * input.throttle() * Time.deltaTime;

        pmodel.Rotate(wheel.rpm, wheel.transform.rotation.y, 0); //Rotate player model based on RPM
        //This is a purely visual rotation, could be used to 'animate' as a gyroscope.
        //Wheel collider will need something separate in this case (OR remove the Y component and set as 0 in above method call)
        

        //Turning
        if(input.direction() > 0)
        {
            turn += turnRate;
            
        }
        else if (input.direction() < 0)
        {
            turn -= turnRate;
        }

        if(turn >= 360 || turn <= -360) //Values outside this range are inoperative
        {
            turn = 0;
        }

  
        ///pmodel.transform.Rotate(new Vector3(pmodel.transform.eulerAngles.x, turn, 0));
       // pmodel.localRotation = new Quaternion(pmodel.transform.rotation.x, turn / 180, pmodel.transform.rotation.z, pmodel.transform.rotation.w);

   

        wheel.steerAngle = turn; //Set angle in degrees
        
        
    }
}
