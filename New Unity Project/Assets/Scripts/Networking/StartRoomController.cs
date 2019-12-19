////SCRIPT WRITTEN BY RICKY JAMES - Last updated 18/12/2019
////Using Photon tutorial by InfoGamer https://www.youtube.com/watch?v=02P_mrszvzY
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartRoomController : MonoBehaviourPunCallbacks
{
    private const int waitingRoomSceneIndex = 1;


    public override void OnEnable()
    {
        //Register to photon callbacks
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        //Remove from callback functions
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    //Callback for joining or creating a room
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined waiting room");
        UnityEngine.SceneManagement.SceneManager.LoadScene(waitingRoomSceneIndex);
    }



}
