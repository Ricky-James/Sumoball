using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float throttle()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public float direction()
    {
        return Input.GetAxisRaw("Horizontal");
    }



   
}
