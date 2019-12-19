////SCRIPT WRITTEN BY RICKY JAMES - Last updated 18/12/2019
////Using Photon tutorial by InfoGamer https://www.youtube.com/watch?v=02P_mrszvzY
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class WaitingRoomController : MonoBehaviourPunCallbacks
{
    //A lot of confusing variable names
    /// <summary>
    /// For some clarity
    /// A timeR is a running timer, generally decreasing by delta time
    /// a "time" is a max time (const) the timer will reset to under certain circumstances
    /// One timer will decrease with only 2 players (or minPlayers var)
    /// One timer will decrease with a full lobby (short countdown)
    /// </summary>

    private PhotonView myPhotonView;
    private const int multiplayerSceneIndex = 2;
    private const int menuSceneIndex = 0;

    private int playerCount; //# of active players
    private const int maxPlayers = 4; //Size of room
    [SerializeField] private int minPlayersToStart;

    //UI Variables
    [Header("UI Variables")]
    [SerializeField] private Text playerCountText;
    [SerializeField] private Text timeToStartText;

    //Bool var for timer
    private bool minimumLobby; //For when min players is met but not max players
    private bool fullLobby; //For max players, starts short countdown
    private bool startingGame;
    //Countdown floats - This can be cleaned up dramatically but only if time permits...
    private float timerToStartGame; //Game starts if hits 0, gets set to full or not full timer on circumstances
    //Timer reset variables
    private const float fullLobbyResetTime = 5;
    private const float unfilledLobbyResetTime = 30;


    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();

        ResetTimer();
        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        //Count of all current players
        playerCount = PhotonNetwork.PlayerList.Length;

        playerCountText.text = (playerCount + "/" + maxPlayers);

        if(playerCount == maxPlayers)
        {
            fullLobby = true;
            minimumLobby = false;
        } else if (playerCount >= minPlayersToStart)
        {
            minimumLobby = true;
            fullLobby = false;
        }
        else
        {
            minimumLobby = false;
            fullLobby = false;
        }

    }

    //Player parameter not to be confused with custom class Player that is not part of the Photon API
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        PlayerCountUpdate();
        ResetTimer();

        if (PhotonNetwork.IsMasterClient)
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {

        //if (timeIn < timerToStartGame)
        //{
        //    timerToStartGame = timeIn;
        //}
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PlayerCountUpdate();
        ResetTimer();
    }

    private void Update()
    {
        WaitingForPlayers();
    }

    private void WaitingForPlayers()
    {
        if (playerCount <= 1)
            ResetTimer();
        else
        {
            timerToStartGame -= Time.deltaTime;
        }

        if (fullLobby && timerToStartGame > fullLobbyResetTime) //Full room so timer gets reduced to 5
        {
            timerToStartGame = fullLobbyResetTime;
        }
        

        timeToStartText.text = string.Format("{0:00}", timerToStartGame);

        if(timerToStartGame <= 0)
        {
            //So only 1 player starts
            if (startingGame)
                return;
            StartGame();
        }
    }

    private void ResetTimer()
    {
        //Reset timers to initial values
        timerToStartGame = unfilledLobbyResetTime;

    }

    //Ran when timer reaches 0
    private void StartGame()
    {
        startingGame = true;
        //Only master client continues
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false; //Close room so more players can't join
        PhotonNetwork.LoadLevel(multiplayerSceneIndex); //Start multiplayer scene

    }

    //Cancel button function
    public void WaitRoomCancel()
    {
        ResetTimer();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

}
