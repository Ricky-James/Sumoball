////SCRIPT WRITTEN BY RICKY JAMES - Last updated 14/12/2019
using UnityEngine;

public class AliveState : PlayerState
{

    private const float forwardSpeed = 10;
    private const float strafeSpeed = 5;

    public AliveState(Player player) : base(player)
    {
        Debug.Log("Alive State");
        player.rb.constraints = RigidbodyConstraints.None;
    }

    public override void handleInput()
    {
        player.rb.AddForce(player.cam.forward * forwardSpeed * Input.GetAxis("Vertical"));
        player.rb.AddForce(player.cam.right * strafeSpeed * Input.GetAxis("Horizontal"));
    }

    //Basically update, called in gamestates
    public override void Tick()
    {
        handleInput();
    }
}
