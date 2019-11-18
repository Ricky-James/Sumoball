using UnityEngine;


public class playerInfo : MonoBehaviour
{
    public int lives { get; private set; } = 3;

    public void reduceLives()
    {
        lives--;
        Debug.Log("Lives: " + lives);
    }
    
    



}
