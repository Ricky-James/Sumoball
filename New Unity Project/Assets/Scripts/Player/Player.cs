////SCRIPT WRITTEN BY RICKY JAMES - Last updated 19/12/2019
///Largely utilising scripts from:
//https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Player : MonoBehaviourPun
{

    
    public Rigidbody rb;
    [HideInInspector] public GameObject cam;
    public Transform camTarget;
    [HideInInspector] public Transform respawnLocation;
    public int lives; //Set in inspector
    public TMPro.TextMeshProUGUI lifeText;
    public TMPro.TextMeshProUGUI countdownText;

    
    public PhotonView PV;
    public static int playerID;

    private PlayerState currentState;

    //Cam target variables
    private const float rotSpeed = 1.8f;
    private const float distance = 10f; //distance from player (radius of orbit)
    private float timer; //Used for smooth trig
    private float xPos, zPos;
    private Vector3 targetOffset;
    float direction;

    private bool playerSetup = false;
  

    void Start()
    { 
        // 0 for debugging. Players will normally always be numbered from 1 upwards
        if (PV.IsMine || PV.OwnerActorNr == 0)
        {
            //Run setup only once (Start() is called on every state change)
            if (!playerSetup)
            {
                playerInit();
                playerSetup = true;
            }
                

        }

    }

    void Update()
    {

        if (photonView.IsMine || PV.OwnerActorNr == 0)
        {
            currentState.Tick();
        }
    }

    public void playerInit()
    {
        //Find the camera in the scene
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        rb.gameObject.AddComponent<PlayerCollisions>();
        lifeText = GameObject.Find("Lives text").GetComponent<TMPro.TextMeshProUGUI>();
        countdownText = GameObject.Find("321Go").GetComponent<TMPro.TextMeshProUGUI>();

        //Converts player number to the layer they should be on (player 1,2,3,4 = layer 8,9,10,11)
        //Players are on unique layers to cull themselves
        int layerNumber = PV.OwnerActorNr + 7;
        //Change layer the player is on so that the camera can cull its own player model
        //without culling all players
        gameObject.layer = layerNumber;
        rb.gameObject.layer = layerNumber;

        cam.layer = layerNumber;
        camTarget.gameObject.layer = layerNumber;

        //Enable all culling masks but NOT the bit the player is on
        cam.GetComponent<Camera>().cullingMask = ~(1 << layerNumber);
        cam.GetComponent<Camera>().enabled = true;

        respawnLocation = GameObject.FindGameObjectWithTag("Respawn").transform;
        SetState(new SpawnState(this));


    }

    public void SetState(PlayerState state)
    {
        currentState = state;
    }
    

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
        }

        else
        {
            rb.position = (Vector3)stream.ReceiveNext();
            rb.rotation = (Quaternion)stream.ReceiveNext();
        }
    }


    //Might be better to move this to a seperate component
    //This gets ran in the spawn and alive state update ticks
    public void handleCamera()
    {
        //Inputs left/right make a target orbit the player
        //Player moves towards this target in handleInput()
        //Camera always faces this target
        direction = Input.GetAxisRaw("Horizontal");
        timer -= Time.deltaTime * rotSpeed * direction;


        xPos = Mathf.Cos(timer) * distance;
        zPos = Mathf.Sin(timer) * distance;

        targetOffset = new Vector3(xPos, 0, zPos);

        //Target object orbits player
        camTarget.position = rb.position + targetOffset;

        //Camera position is same as player with small Y elevation
        //Camera always looks at target (which orbits the player)
        cam.transform.position = new Vector3 (rb.position.x, rb.position.y + 1.5f, rb.position.z);
        cam.transform.LookAt(camTarget);

    }

}
