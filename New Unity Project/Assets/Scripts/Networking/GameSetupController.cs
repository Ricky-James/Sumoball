////SCRIPT WRITTEN BY RICKY JAMES - Last updated 18/12/2019
////Using Photon tutorial by InfoGamer https://www.youtube.com/watch?v=02P_mrszvzY
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupController : MonoBehaviour
{
    public GameObject BirdCam;
    public GameObject DeathCam;

    private void Start()
    {

        BirdCam.SetActive(true);
        DeathCam.SetActive(true);

        CreatePlayers();

    }

    private void CreatePlayers()
    {

        
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonNetworkPlayer"), Vector3.zero, Quaternion.identity);

    }





}
