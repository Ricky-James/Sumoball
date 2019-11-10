using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCameraOld : MonoBehaviour
{ 
    //Target player parent object
    public GameObject target;
    private Vector3 offset;

    //private const float mouseSensitivity = 3f;

    private float mouseX;

    public bool LookAtPlayer = false;
    public bool RotateAroundPlayer = true;
    public float RotationSpeed = 5.0f;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;






    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;

    }


    private void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        cameraUpdate();


    }


    private void cameraUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = target.transform.position + offset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor); //NTS: Research Slerp

            if (RotateAroundPlayer)
            {
                Quaternion camTurnAngle =
                    Quaternion.AngleAxis((mouseX * RotationSpeed), Vector3.up);

                offset = camTurnAngle * offset;

            }

            if (LookAtPlayer || RotateAroundPlayer)
            {
                transform.LookAt(target.transform);
            }
        }
    }
}
