////SCRIPT WRITTEN BY RICKY JAMES - Last updated 18/12/2019
////Using Photon tutorial by InfoGamer https://www.youtube.com/watch?v=02P_mrszvzY
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    //Buttons to start/stop searching for a game
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject CancelButton;
    [SerializeField] private const int maxPlayers = 4;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //Enables start button once connection is established
        StartButton.SetActive(true);
    }

    //Button click event
    public void LobbyStart()
    {
        StartButton.SetActive(false);
    //    CancelButton.SetActive(true); Cancel button currently bugged
    //    I think issue is not sending RPC calls that the player has left the room
        PhotonNetwork.JoinRandomRoom(); ; //Try to join an existing room
    }

    //Function runs on any error for failing to join a room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room. Error: " + returnCode + ": " + message);
        //32760 = No match found
        //Create a room if the reason for failing is that no rooms exist
        if(returnCode == 32760)
        {
            CreateRoom(); //Retry until successful
        }
    }

    void CreateRoom() //Create our own room
    {
        int roomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)maxPlayers };
        PhotonNetwork.CreateRoom("Room: " + roomNumber, roomOps);
        Debug.Log("Room number: " + roomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room, attempting again...");
        CreateRoom();
    }

    //Cancel button click
    public void LobbyCancel()
    {
        PhotonNetwork.LeaveRoom();
        CancelButton.SetActive(false);
        //Re-enable start button after leaving
        StartButton.SetActive(true);
        
    }
}
