using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private RaycastHit hitObj; //Holds object ID of anything Raycast hits
    private float rayLength; //Raycast line length
    public GameObject player;
    public GameObject bullet;
    public GameObject other;
    private int ricochet;
    private Vector3 rayPosition;
    private Vector3 rayDirection;
    private Color rayColour;
    private float newRayLength;
    //  private Color rayColour;
    // private Vector3 rayDirection;

    void Start()
    {
        ricochet = 4;
        GameObject player = GameObject.Find("Weapon");
        rayLength = 70.0f;
        newRayLength = rayLength;
    }

    void Update()
        {
        if (Input.GetKey(KeyCode.Space))
            {
            Instantiate(bullet, player.transform);
            bullet.transform.position = Vector3.MoveTowards(rayPosition, hitObj.point, 0.005f);
        }
        }
    void OnDrawGizmos()
    {
        if (ricochet <= 0)
        {
            return;
        }
        else
        {
            rayColour = Color.red; //changes raycast colour
            rayDirection = player.transform.forward;  //sets direction of raycast to player's z axis
            rayPosition = player.transform.position;  //makes raycast position that of the player game objects
            Debug.DrawRay(rayPosition, player.transform.forward * rayLength, rayColour, 0, true);
            //ricochet -= 1;
            if (Physics.Raycast(rayPosition, player.transform.forward, out hitObj, rayLength))
            {
                rayLength = Vector3.Distance(rayPosition, hitObj.point); //Changes Raycast length the distance between the starting point and point of object collision
                if (hitObj.collider.tag == "Wall")
                {
                    //ricochet -= 1;
                    rayColour = Color.blue;
                    rayPosition = hitObj.point; //Makes new Raycast starting position the last point of collision
                    rayDirection = Vector3.Reflect(rayDirection, hitObj.normal); //TO DO - Find out how to do this manually via trig
                    Debug.DrawRay(rayPosition, rayDirection * newRayLength, rayColour, 0, true); //draws new raycast from last point on the tangent of the previous raycast
                                                                                                 //draws a line from raycast using starting position, end/direction and colour
                }
            }
            else
            {
                rayLength = 70.0f;
                ricochet = 4;
            }
        }

    }
    }


