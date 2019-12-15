////SCRIPT WRITTEN BY RICKY JAMES - Last updated 15/12/2019
//Initiated when player has <0 lives
using UnityEngine;

public class GameOverState : PlayerState
{
    public GameOverState(Player player) : base(player)
    {

        Component.FindObjectOfType<Camera>().enabled = true;
        //Destroy all attached objects except Player (this)
        for(int x = 0; x < player.transform.childCount; x++)
        {
            player.lifeText.text = "Game Over.";
            player.countdownText.enabled = false;
            GameObject.Destroy(player.transform.GetChild(x).gameObject);
        }
    }


    public override void Tick()
    {
        

    }
}

