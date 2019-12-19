using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject startButton;
    public GameObject cancelButton;
    public GameObject loadingText;

    private void Awake()
    {
        lobby = this; //Singleton in main menu scene
    }

    private void Start()
    {
        loadingText.SetActive(true);
        PhotonNetwork.ConnectUsingSettings(); ;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
        loadingText.SetActive(false);
    }

    public void onStartButtonClick()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void onCancelButtonClick()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomName = Random.Range(0,10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)MultiplayerSettings.maxPlayers
        };
        PhotonNetwork.CreateRoom("Room #" + randomRoomName, roomOps);

    }

    public override void OnJoinedRoom()
    {
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed, Error: " + + returnCode + message);
        Debug.Log("Trying again");
        CreateRoom();
    }
}
