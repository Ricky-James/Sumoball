////SCRIPT WRITTEN BY RICKY JAMES - Last updated 15/12/2019
//State used as player phases in to spawn after dying/start of round.
using UnityEngine;



public class SpawnState : PlayerState
{

    private Vector3 spawnOffset = new Vector3(0, 3.5f, 0);

    private Vector3[] initialSpawns =
{
        new Vector3(21,  3.5f, 0),
        new Vector3(-21, 3.5f, 0),
        new Vector3(0,   3.5f, 21),
        new Vector3(0,   3.5f, -21)
    };

    private const float spawnTime = 4.0f;
    float timeUntilAlive = 4.0f;

    public SpawnState(Player player) : base(player)
    {
        //0 owner for debugging
        if (player.PV.IsMine || player.PV.OwnerActorNr == 0)
        {
            Debug.Log("Spawn State");

            reduceLives(); //Also checks for game over

            if (player.lives == 3 && player.PV.OwnerActorNr != 0) //Initial spawn points
            {
                player.rb.position = initialSpawns[player.PV.OwnerActorNr - 1];
            }
            else //Random spawn point
            {
                //Setting to kinematic before moving the player to spawn point prevents player from getting stuck
                player.rb.isKinematic = true;
                player.rb.position = player.respawnLocation.position;


            }


            timeUntilAlive = spawnTime;
        }

    }

    public override void Tick()
    {
        player.rb.velocity = Vector3.zero;
        player.rb.angularVelocity = Vector3.zero;

        player.handleCamera();
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
            player.rb.isKinematic = false;
            player.SetState(new AliveState(player));
        }
        

            

    }

    public void reduceLives()
    {
        player.lives--;
        player.lifeText.text = "Lives: " + player.lives;

        if (player.lives <= 0)
            player.SetState(new GameOverState(player));
    }

}
