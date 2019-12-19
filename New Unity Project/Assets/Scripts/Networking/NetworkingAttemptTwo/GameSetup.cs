using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;

    private Vector3[] initialSpawns =
{
        new Vector3(21,  2.75f, 0),
        new Vector3(-21, 2.75f, 0),
        new Vector3(0,   2.75f, 21),
        new Vector3(0,   2.75f, -21)
    };


    private void OnEnable()
    {
        if (GameSetup.GS == null)
            GameSetup.GS = this;
    }
}
