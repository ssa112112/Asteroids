using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    static AudioSource fon;

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource fon)
    {
        AudioManager.fon = fon;
        audioClips.Add(AudioClipName.AsteroidHit, 
            Resources.Load<AudioClip>("hit"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("die"));
        audioClips.Add(AudioClipName.PlayerShot,
            Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioClipName.StartGame,
            Resources.Load<AudioClip>("start"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        AudioSource.PlayClipAtPoint(audioClips[name], Vector3.zero);
    }

    public static void PlayFon()
    {
        fon.Play();
    }

    public static void DontPlayFon()
    {
        fon.Stop();
    }
}
