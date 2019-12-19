//SCRIPT WRITTEN BY RICKY JAMES - Last updated 11/11/2019
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject overCam;
    public arenaDoor[] middleDoors;
    public int middleDoorFrequency;
    private float doorTimer;
    private bool doorsEnabled;

    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            doorsEnabled = false;
            overCam.SetActive(true);
        }
        else
        {
            doorsEnabled = true;
            overCam.SetActive(false);
        }
        doorTimer = middleDoorFrequency;

        //Hides cursor:
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorsEnabled)
        {
            doorTimer -= Time.deltaTime;
            if (doorTimer <= 0)
            {
                doorTimer = middleDoorFrequency + 7.5f; //Additional 7.5 seconds to account for opening/closing animation
                foreach (arenaDoor door in middleDoors)
                {
                    door.enabled = true;
                }
            }
        }

    }
}
