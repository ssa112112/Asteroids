using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager texts
/// </summary>
public class HUG : MonoBehaviour
{
    [SerializeField]
    Text score;
    [SerializeField]
    Text central; 

    //Time of game
    private float seconds;

    //true, when the game running
    bool working = false;

    void Update()
    {
        if (working)
        {
            seconds += Time.deltaTime;
            score.text = ((int)seconds).ToString();
        }
    }

    /// <summary>
    /// Stop the timer and show the final message
    /// </summary>
    public void StopGame()
    {
        score.text = "Final result: " + score.text;
        ChangeCentralText("PRESS [F5] FOR RESTART");
        working = false;
    }

    public void StartGameTimer()
    {
        working = true;
    }

    public void ChangeCentralText(string newText)
    {
        central.text = newText;
    }
}
