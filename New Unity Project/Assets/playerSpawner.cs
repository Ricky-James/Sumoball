using System.Collections;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{

    private Vector3 spawnLocation;
    private const float spawnDistance = 1.78f; //Distance from ground to spawner (Prevents spawning on slopes)
    //Spawn distance is very precise with the spawner being 2.75 units in the air
    //This prevents spawning on even the smallest incline


    private void OnTriggerStay(Collider other)
    {
        moveSpawner();
    }

    private void OnCollisionStay(Collision collision)
    {
        moveSpawner();
    }

    


    private void isSafe() //Check to ensure the spawn point has ground beneath
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, Vector3.down, out hit, spawnDistance))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * 1000, Color.white);
            moveSpawner();
        }


    }

    private void moveSpawner()
    {
        
        //Randomised spawn co-ords to cover the map
        int x = Random.Range(-22, 23);
        int z = Random.Range(-22, 23);
        spawnLocation = new Vector3(x, transform.position.y, z);

        transform.SetPositionAndRotation(spawnLocation, Quaternion.LookRotation(Vector3.zero));
        Debug.Log("Spawn moved!");

    }

    private void Update()
    {
        isSafe(); //Constantly checks to see if there is ground beneath the spawner.
        //This wouldn't be necessary to constantly check if the middle doors didn't regularly open and close

        
    }








}
