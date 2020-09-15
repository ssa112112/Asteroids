using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An audio source for the entire game
/// </summary>
public class GameAudioSource : MonoBehaviour
{
	void Awake()
    {
        //AudioSourse is fon
        AudioManager.Initialize(GetComponent<AudioSource>());
    }
}
