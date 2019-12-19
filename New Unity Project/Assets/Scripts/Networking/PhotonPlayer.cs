////SCRIPT WRITTEN BY RICKY JAMES - Last updated 18/12/2019
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Essentionally just the network connection for player

public class PhotonPlayer : MonoBehaviour
{

    private PhotonView PV;


    private void Start()
    {
        PV = GetComponent<PhotonView>();

        if(PV.IsMine)
        {
            CreatePlayer();
        }

    }

    private void CreatePlayer()
    {

        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), Vector3.up * 3, Quaternion.identity, 0);


    }
}
