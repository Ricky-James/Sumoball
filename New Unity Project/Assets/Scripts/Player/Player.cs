////SCRIPT WRITTEN BY RICKY JAMES - Last updated 13/12/2019
///Largely utilising scripts from:
//https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody rb;
    public Transform cam;

    private PlayerState currentState;

    void Start()
    {
        SetState(new AliveState(this));
    }

    void Update()
    {
        currentState.Tick();
    }

    public void SetState(PlayerState state)
    {
        currentState = state;
        gameObject.name = "Player " + state.GetType().Name;
    }

}
