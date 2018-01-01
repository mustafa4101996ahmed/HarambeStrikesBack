using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCountManager : MonoBehaviour
{
    Text text;
    public static int totalKills;

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
    }


    void Update()
    {
        text.text = totalKills.ToString();
        // Set the displayed text to be the word "Score" followed by the score value.
    }
}


