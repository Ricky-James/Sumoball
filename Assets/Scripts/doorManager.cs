using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorManager : MonoBehaviour
{

    private const float middleTimer = 10;

    public GameObject[] middleDoors;
    private float timer;
    private bool centerOpen;

    //Closed position for 2 doors
    private Vector3 closedPos0;
    private Vector3 closedPos1;

    //Open positions for 2 doors
    private Vector3 openPos0;
    private Vector3 openPos1;

    private void Start()
    {
        timer = middleTimer;
        centerOpen = false;

        //Set target positions
        closedPos0 = middleDoors[0].transform.position;
        closedPos1 = middleDoors[1].transform.position;

        openPos0 = new Vector3(middleDoors[0].transform.position.x, middleDoors[0].transform.position.y, -18.08f);
        openPos1 = new Vector3(middleDoors[1].transform.position.x, middleDoors[0].transform.position.y, 17.47f);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
           // timer = middleTimer;
            middleHole();
        }
    }

    void middleHole()
    {


        //if(centerOpen)
        //{
        //    middleDoors[0].transform.position = Vector3.MoveTowards(middleDoors[0].transform.position, closedPos0, Time.deltaTime);
        //    middleDoors[1].transform.position = Vector3.MoveTowards(middleDoors[1].transform.position, closedPos1, Time.deltaTime);
        //    centerOpen = false;
        //    Debug.Log("Closing");
        //}
        //else
        //{
        //    middleDoors[0].transform.position = Vector3.MoveTowards(middleDoors[0].transform.position, openPos0, Time.deltaTime);
        //    middleDoors[1].transform.position = Vector3.MoveTowards(middleDoors[1].transform.position, openPos1, Time.deltaTime);
        //    centerOpen = true;
        //    Debug.Log("Opening");
        //}
    }
}
