////SCRIPT WRITTEN BY RICKY JAMES - Last updated 14/12/2019
//State used as player phases in to spawn after dying/start of round.
using UnityEngine;

public class SpawnState : PlayerState
{
    private const float spawnTime = 4.0f;
    float timeUntilAlive = 4.0f;
    private bool respawning = false;


    public SpawnState(Player player) : base(player)
    {
        Debug.Log("Spawn State");

        reduceLives(); //Also checks for game over
        

        //Reset velo to stop player from rolling around while spawning
        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;
        //Prevent player from being knocked back while spawning
        player.rb.constraints = RigidbodyConstraints.FreezeAll;
        //Set player to respawn location
        //Rotation doesn't matter as the player will face the cam target on next tick
        player.rb.transform.SetPositionAndRotation(player.respawnLocation.position, Quaternion.identity);

        timeUntilAlive = spawnTime;
    }

    public override void Tick()
    {
        if(respawning)
        {
            player.countdownText.enabled = true;
            timeUntilAlive -= Time.deltaTime;

            if (timeUntilAlive > 1)
            {
                //Cast countdown to int to floor it / remove decimals
                player.countdownText.text = ((int)timeUntilAlive).ToString() + "...";
            }
            else
            {
                player.countdownText.text = "Sumo!";
            }


            if (timeUntilAlive < 0)
            {
                player.countdownText.enabled = false;
                respawning = false;
                player.SetState(new AliveState(player));
            }
        }

            

    }

    public void reduceLives()
    {
        player.lives--;
        player.lifeText.text = "Lives: " + player.lives;
        respawning = player.lives > 0 ? true : false;

        if (player.lives <= 0)
            player.SetState(new GameOverState(player));
    }

}
