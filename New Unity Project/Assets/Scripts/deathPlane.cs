//SCRIPT WRITTEN BY RICKY JAMES - Last updated 19/11/2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathPlane : MonoBehaviour
{

    public Transform respawner; 
    private Queue<Transform> respawnQueue = new Queue<Transform>();

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Player")
        {
            Player p = col.GetComponentInParent<Player>();
            p.SetState(new SpawnState(p));
        }
        else Destroy(col); //Destroys items etc.

    }



 

}
