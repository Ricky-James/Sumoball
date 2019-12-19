////SCRIPT WRITTEN BY RICKY JAMES - Last updated 14/12/2019
using UnityEngine;

public class AliveState : PlayerState
{
    //Player essentially moves towards a gameobject that orbits the player
    //Orbiting is controled by player

    //Movement
    private const float forwardSpeed = 30;
    private const float strafeSpeed = 5;
    




    public AliveState(Player player) : base(player)
    {
        Debug.Log("Alive State");
        player.rb.constraints = RigidbodyConstraints.None;
       
    }

    public override void handleInput()
    {
        
        player.rb.AddForce(Camera.main.transform.forward * forwardSpeed * Input.GetAxis("Vertical"));
        player.rb.AddForce(Camera.main.transform.right * strafeSpeed * Input.GetAxis("Horizontal"));


    }



    //Basically update, called in gamestates
    public override void Tick()
    {

        handleInput();
        player.handleCamera();
       

    }
}
