using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoom room;
    private PhotonView PV;
    public bool isGameLoaded;
    public int currentScene;

    //Player info
    private Photon.Realtime.Player[] photonPlayers;
    private int playersInRoom;
    private int myNumberInRoom;
    private int playerInGame;

    //Timers for delay start
    private bool isReadyToCount; //isWaitingForPlayers?
    private bool isReadyToStartGame;
    public float waitingTimeForPlayers;
    private float lessThanMaxPlayersCountdown;
    private float maxPlayersCountdown;
    private float timeToStart;

    //UI
    public Text playerCountText;
    public Text countdownText;

    //Singleton
    private void Awake()
    {
        if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            if(PhotonRoom.room != this)
            {
                //Replace current instance if one exists
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
    }

    //Setup event listener
    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this); ;
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    //Initialising vars
    private void Start()
    {
        playerCountText.text = playersInRoom + "/" + MultiplayerSettings.maxPlayers;
        PV = GetComponent<PhotonView>();
        isReadyToCount = false;
        isReadyToStartGame = false;
        lessThanMaxPlayersCountdown = timeToStart; //Long countdown if room is not full
        maxPlayersCountdown = 5.0f; //Short countdown if the room is full
        timeToStart = waitingTimeForPlayers; 

        
    }

    private void Update()
    {
        //Confusing timer code, different timers for delay starting
        //Short timer if room is full
        if(MultiplayerSettings.isDelayStart)
        {
            if(playersInRoom == 1)
            {
                RestartTimer();
            }
            if(!isGameLoaded)
            {
                if(isReadyToStartGame)
                {
                    maxPlayersCountdown -= Time.deltaTime;
                    lessThanMaxPlayersCountdown = maxPlayersCountdown;
                    timeToStart = maxPlayersCountdown;
                }
                else if (isReadyToCount)
                {
                    lessThanMaxPlayersCountdown -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayersCountdown;
                }
                countdownText.text = string.Format("{0:00}", timeToStart);
                if (timeToStart <= 0)
                    StartGame();
            }
        }
    }

    //Countdown starts if >1 player
    //Start game if not a delay start (immediate on 2 players)
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //Numbering each player
        playersInRoom = PhotonNetwork.PlayerList.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        playerCountText.text = playersInRoom + "/" + MultiplayerSettings.maxPlayers;
        if (MultiplayerSettings.isDelayStart)
        {

            if (playersInRoom > 1)
                isReadyToCount = true;
            if (playersInRoom == MultiplayerSettings.maxPlayers)
            {
                isReadyToStartGame = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        else
        {
            StartGame();
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        playersInRoom = PhotonNetwork.PlayerList.Length;
        myNumberInRoom = playersInRoom;
        playerCountText.text = playersInRoom + "/" + MultiplayerSettings.maxPlayers;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        if (MultiplayerSettings.isDelayStart)
        {
            if (playersInRoom > 1)
                isReadyToCount = true;
            if (playersInRoom == MultiplayerSettings.maxPlayers)
            {
                isReadyToStartGame = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        if(MultiplayerSettings.isDelayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.sumoScene);
    }

    void RestartTimer()
    {
        lessThanMaxPlayersCountdown = waitingTimeForPlayers;
        timeToStart = waitingTimeForPlayers;
        maxPlayersCountdown = 5;
        isReadyToCount = false;
        isReadyToStartGame = false;
    }


    //Called when multiplayer scene is loaded
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.buildIndex;
        if(currentScene == MultiplayerSettings.sumoScene)
        {
            isGameLoaded = true;
            if(MultiplayerSettings.isDelayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            else
            {
                RPC_CreatePlayer();
            }
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        //Check for duplicate players
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        //Network player is not a physical player, more like a controller
        PhotonNetwork.Instantiate(System.IO.Path.Combine("Prefabs", "PhotonNetworkPlayer"), Vector3.zero, Quaternion.identity, 0);
    }

}
