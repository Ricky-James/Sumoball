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
    [SerializeField] private const int maxPlayers = 4; //Number of players in the room (manual setting)

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //Enables start button once connection is established
        StartButton.SetActive(true);
    }

    //Button click
    public void LobbyStart()
    {
        StartButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); ; //Try to join an existing room
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room. Error: " + returnCode + ": " + message);
        //No match found
        if(returnCode == 32760)
        {
            CreateRoom();
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

    public void LobbyCancel()
    {
        CancelButton.SetActive(false);
        StartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
