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
        int playerID = PV.OwnerActorNr % PhotonNetwork.CurrentRoom.PlayerCount;

        Quaternion initRotation = new Quaternion();
        switch (playerID)
        {
            case 0:
                initRotation = Quaternion.identity;
                break;
            case 1:
                initRotation = new Quaternion(0, 180, 0, 0);
                break;
            case 2:
                initRotation = new Quaternion(0, 90, 0, 0);
                break;
            case 3:
                initRotation = new Quaternion(0, 270, 0, 0);
                break;
            default:
                initRotation = Quaternion.identity;
                break;
        };



        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), SpawnState.initialSpawns[playerID], initRotation, 0);

    }
}
