//SCRIPT WRITTEN BY RICKY JAMES - Last updated 19/11/2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{

    public Transform respawner; //
    private Queue<GameObject> respawnQueue = new Queue<GameObject>();

    private bool respawning = false;
    private bool playerInQueue = false;

    private void Update()
    {
        if(respawnQueue.Count > 0 && respawning == false)
        {
            respawning = true;
            StartCoroutine("respawnPlayer");
        }

       
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {
            //Dead players are added to a queue when they die
            respawnQueue.Enqueue(col.gameObject);

        }
        else Destroy(col); //Destroys items etc.

    }

    public IEnumerator respawnPlayer()
    {
        //Set properties of player to be spawned
        GameObject player = respawnQueue.Dequeue();
//        player.GetComponent<Player>().SetState(new RespawnState());


        Rigidbody rb = player.GetComponent<Rigidbody>(); //Used to reset momentum
        Transform parent = player.GetComponentInParent<Transform>(); //Used to set position to spawn loc
        playerInfo info = player.GetComponentInParent<playerInfo>(); //Used to check/deduct lives
        playerMovement movement = player.GetComponent<playerMovement>();

        if (info.lives < 0)
        {
            respawning = false;
            yield return null; //Exit coroutine if player is out of lives
        }
            
        //Reset momentum
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll; //Prevent player from being knocked back while spawning
        
        
        //Set pos to random spawner pos
        //Rotation doesn't matter as the player will face the cam target on next update step
        parent.transform.SetPositionAndRotation(respawner.position, Quaternion.identity);

        //Movement disables for 3 seconds
        yield return new WaitForSeconds(3);

        info.reduceLives();
        movement.enabled = true;
        rb.constraints = RigidbodyConstraints.None;
        respawning = false;
        Debug.Log("Players in queue: " + respawnQueue.Count);
       
        
        yield return null;
    }



 

}
