using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDirections : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
  //      this.transform.position = player.transform.position;
        this.transform.SetPositionAndRotation(player.transform.position,
            new Quaternion(player.transform.rotation.x, player.transform.rotation.y, 0, 0));
    }

}
