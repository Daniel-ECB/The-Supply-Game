using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelData : MonoBehaviour
{
    public AudioClip sceneMusic;

    // Use this for initialization
    void Start()
    {
        AudioMaster.instance.CurrentMusicClip = sceneMusic;
    }

    /// <summary>
    /// Plays the click sounds.
    /// </summary>
    /// <param name="soundClip">The click sound to play.</param>
    public void ClickSound(AudioClip soundClip)
    {
        AudioMaster.instance.PlaySpecialEffect(soundClip);
    }
}
