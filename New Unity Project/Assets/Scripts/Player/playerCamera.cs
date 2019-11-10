using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private InputManager input;
    [SerializeField] private Transform camTarget;
    
    

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        transform.LookAt(camTarget);

        transform.position = new Vector3(player.position.x, player.position.y + 1.5f, player.position.z);

        //yPos += ((Mathf.Cos(transform.rotation.y) * rotSpeed));

        //transform.Rotate(new Vector3(transform.rotation.x, yPos, transform.rotation.z) * input.direction());

        ///SUMMARY
        ///Below are several scrapped rotation methods, some worked, some did not.
        ///Keeping them in script for now in case I decide to report on them for assignment


        //targetYAngle = input.direction() * 90;
        //currentYAngle = Mathf.MoveTowardsAngle(currentYAngle, targetYAngle, rotSpeed * Time.deltaTime);
        //camTarget.position = new Vector3(pivot.x + Mathf.Sin(currentYAngle * Mathf.Deg2Rad) * distance, transform.position.z, pivot.z + Mathf.Cos(currentYAngle * Mathf.Deg2Rad) * distance);



        
        //Works but doesn't stop when 0 input. Also doesn't sync with actual facing direction
        ///camTarget.RotateAround(transform.position, Vector3.up,  Time.deltaTime * input.direction());
       // transform.LookAt(camTarget);

        


        ///Works..ish? Snappy camera, possibly due to quaternion value and/or X rotation
        //   transform.SetPositionAndRotation(player.position + player.TransformDirection(0, 0, 0), 
        //       new Quaternion(transform.rotation.x, playerModel.rotation.y, transform.rotation.z, transform.rotation.w));


        //    transform.LookAt(new Vector3(transform.localPosition.x, transform.localPosition.y, playerModel.localPosition.z + 1));

        ///Has some sort of functionality, has complications with other axes
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerModel.eulerAngles.y, transform.eulerAngles.z);



        //transform.Rotate((transform.up * horizontal) * playerControl.rotationSpeed * Time.deltaTime);


        ///<summary>
        /// Commented out a restricted-cam view that would sort of simulate VR
        /// Couldn't full get it to function and spend way too long on it
        /// A restricted view is perfectly acceptable now until VR implementation
        /// 
        /// 
        ///mouseX = Input.GetAxis("Mouse X") * mouseSensitivty * Time.deltaTime;
        ///mouseY = Input.GetAxis("Mouse Y") * mouseSensitivty * Time.deltaTime;
        ///xRotation -= mouseY;
        ///yRotation += mouseX;



        ///yRotation = Mathf.Clamp(yRotation, -35f, 35f); //Clamp to 70deg vision radius to simulate VR
        ///xRotation = Mathf.Clamp(xRotation, -35f, 35f);
        ///transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        /// 
        /// </summary>



    }


}
