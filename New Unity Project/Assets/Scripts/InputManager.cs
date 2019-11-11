using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float throttle()
    {
        return Input.GetAxis("Vertical");
    }

    public float direction()
    {
        return Input.GetAxis("Horizontal");
    }



   
}
