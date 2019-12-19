using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerSettings : MonoBehaviour
{
    public static MultiplayerSettings multiPlayerSettings;


    [HideInInspector] public const bool isDelayStart = true;
    [HideInInspector] public const int maxPlayers = 4;
    [HideInInspector] public const int mainMenuScene = 0;
    [HideInInspector] public const int sumoScene = 1;

    private void Awake()
    {
        ///Keeps only one instance of MPSettings
        if (MultiplayerSettings.multiPlayerSettings == null)
        {
            MultiplayerSettings.multiPlayerSettings = this;
        }
        else if (MultiplayerSettings.multiPlayerSettings != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
}
