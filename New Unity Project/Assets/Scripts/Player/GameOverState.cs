////SCRIPT WRITTEN BY RICKY JAMES - Last updated 15/12/2019
//Initiated when player has <0 lives
using UnityEngine;

public class GameOverState : PlayerState
{
    public GameOverState(Player player) : base(player)
    {

        if (player.PV.IsMine)
        {
            player.cam.SetActive(false);
            GameObject gameOverCam = GameObject.Find("DeathCamera");
            gameOverCam.tag = "MainCamera";
            player.lifeText.text = "Game Over.";
            player.countdownText.enabled = false;

        }
        GameObject.Destroy(player.gameObject);

    }


    public override void Tick()
    {
        

    }
}

