using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arenaDoor : MonoBehaviour
{

    /// <summary>
    /// Script is enabled by arenaManager. Manager handles timer on frequency of doors opening/closing.
    /// Both doors are handled by this same script.
    /// </summary>
    
    private const float speed = 1.5f; //Decided against access in inspector due to there being 2 doors to change
    private Vector3 zOpenLoc; //+18 or -18
    private Vector3 zClosedLoc;
    private bool isOpen = false;
    
    void Start()
    {
        zClosedLoc = transform.position;
        zOpenLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z * 2.05f); //+18 or -18 depending on door (closed pos is -9.12 and 9.12)
    }

    void Update()
    {


        if (isOpen) //Closes doors if open
        {
            transform.position = Vector3.MoveTowards(transform.position, zClosedLoc, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, zOpenLoc, Time.deltaTime * speed);
        }

        if (transform.position == zOpenLoc) //Marks doors as open and disables script
        {
            isOpen = true;
            enabled = false;
        }
        else if (transform.position == zClosedLoc) //Marks doors as closed and disables script
        {
            isOpen = false;
            enabled = false;
        }


    }

}
