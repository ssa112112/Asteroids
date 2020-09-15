using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    GameObject HUG; //For the future: better use an event system

    HUG HUGscript;

    const float DurationStartMessenge = 8f;

    /// <summary>
    /// Awake is called before Start
    /// </summary>
	void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();
    }

    private void Start()
    {
        HUGscript = HUG.GetComponent<HUG>();
        HUGscript.ChangeCentralText(
@"USE [ARROWS],[SPACE] FOR FLYING
USE [LEFT_CTRL] FOR FIRE
DESTROY ASTEROIDS
SURVIVE");
        AudioManager.Play(AudioClipName.StartGame);
        Invoke("StartGame", DurationStartMessenge);
    }

    private void StartGame()
    {
            HUGscript.ChangeCentralText(string.Empty);
            HUGscript.StartGameTimer();
            gameObject.GetComponent<AsteroidsSpawner>().enabled = true;
            AudioManager.PlayFon();
            Destroy(this);
    }
}
