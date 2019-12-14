//SCRIPT WRITTEN BY RICKY JAMES - Last updated 06/12/2019
//using UnityEngine;

public class playerInfo : UnityEngine.MonoBehaviour
{
    public int lives { get; private set; } = 3;
    public TMPro.TextMeshProUGUI lifeText;

    private void Start()
    {
        lifeText.text = "Lives: " + lives;
    }

    public void reduceLives()
    {
        lives--;
        lifeText.text = "Lives: " + lives;
    }
    
    



}
