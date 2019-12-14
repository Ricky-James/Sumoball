////SCRIPT WRITTEN BY RICKY JAMES - Last updated 13/12/2019
///Largely utilising scripts from:
//https://unity3d.college/2017/05/26/unity3d-design-patterns-state-basic-state-machine/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class, inherit only to create different types of states
public abstract class PlayerState : MonoBehaviour
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }


    public abstract void Tick();
    //Forces basic functionaliy on child objects
    public virtual void handleInput() { }

};

