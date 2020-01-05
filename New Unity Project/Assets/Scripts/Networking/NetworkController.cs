////SCRIPT WRITTEN BY RICKY JAMES - Last updated 16/12/2019
////Using Photon tutorial by InfoGamer https://www.youtube.com/watch?v=02P_mrszvzY
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Documentation https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
//PUN (Photon Unity Networking) is a third party networking tool
//Unity recently made their HLAPI and LLAPI depreciated and haven't yet released something to replace it

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {

        //Connect to Photon servers
        PhotonNetwork.ConnectUsingSettings();
    }

    //Runs when connection is first made
    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully conneted to " + PhotonNetwork.CloudRegion);
    }

}
