using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{
    private playerInfo info;
    private GameObject player;

    //public playerSpawner[] spawners;
    public playerSpawner respawner;
    int randSpawner;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //Additional local references for legibility
            player = col.gameObject;
            info = col.GetComponentInParent<playerInfo>();

            //Select a random spawner
            //This makes player spawns unpredictable and less likely to be abused
           
            if (info.lives > 0)
            {
                info.reduceLives();
                //Resets momentum
                respawnPlayer(ref player);
            }
        }
        else Destroy(col); //Destroys items etc.




    }

    void respawnPlayer(ref GameObject player)
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<playerMovement>().enabled = false;

        //Set pos to random spawner pos
        player.transform.SetPositionAndRotation(respawner.transform.position, Quaternion.LookRotation(Vector3.zero));
    }

 

}
