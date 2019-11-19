//SCRIPT WRITTEN BY RICKY JAMES - Last updated 11/11/2019
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public arenaDoor[] middleDoors;
    public int middleDoorFrequency;
    private float doorTimer;

    void Start()
    {
        doorTimer = middleDoorFrequency;
        //Hides cursor:
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        doorTimer -= Time.deltaTime;
        if(doorTimer <= 0)
        {
            doorTimer = middleDoorFrequency + 7.5f; //Additional 7.5 seconds to account for opening/closing animation
            foreach(arenaDoor door in middleDoors)
            {
                door.enabled = true;
            }
        }
    }
}
