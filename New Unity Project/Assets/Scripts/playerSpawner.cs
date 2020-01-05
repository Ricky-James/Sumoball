//SCRIPT WRITTEN BY RICKY JAMES - Last updated 19/11/2019
using UnityEngine;



public class playerSpawner : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 spawnLocation;

    private const float spawnDistance = 1.78f;
    //Distance from ground to spawner (Prevents spawning on slopes)
    //Spawn distance is very precise with the spawner being 2.75 units in the air
    //This prevents spawning on even the smallest incline

    //Trigger detection is for players, who have a large trigger sphere surrounding them
    //As well as walls/obstacles
    private void OnTriggerEnter(Collider other)
    {
        moveSpawner();
    }

    private void OnTriggerStay(Collider other)
    {
        moveSpawner();
    }

    private void isSafe() //Check to ensure the spawn point has ground beneath
    {
        
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
        int x = Random.Range(-23, 24);
        int z = Random.Range(-23, 24);
        spawnLocation = new Vector3(x, transform.position.y, z);

        transform.SetPositionAndRotation(spawnLocation, Quaternion.identity);


    }

    private void Update()
    {
        isSafe(); //Constantly checks to see if there is ground beneath the spawner.
        //This must be constantly checked as the middle doors
        //regularly open and close

        
    }


}
