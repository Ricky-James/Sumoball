using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Item();
        }
    }

    void Item()
    {
        Debug.Log("pickup");
        Destroy(gameObject);
    }


}
