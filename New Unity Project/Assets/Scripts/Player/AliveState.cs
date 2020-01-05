////SCRIPT WRITTEN BY RICKY JAMES - Last updated 14/12/2019
using UnityEngine;

public class AliveState : PlayerState
{
    //Player essentially moves towards a gameobject that orbits the player
    //Orbiting is controled by player

    //Movement
    private const float forwardSpeed = 160;
    private const float strafeSpeed = 20;
    
    public AliveState(Player player) : base(player)
    {
        Debug.Log("Alive State");
        //Unfreeze the player after respawning
        player.rb.constraints = RigidbodyConstraints.None;
    }

    public override void handleInput()
    {   
        player.rb.AddForce(Camera.main.transform.forward * forwardSpeed * Input.GetAxis("Vertical"));
        player.rb.AddForce(Camera.main.transform.right * strafeSpeed * Input.GetAxis("Horizontal"));
    }

    public override void Tick()
    {
        handleInput();
        player.handleCamera();
    }
}
