using UnityEngine;

public class UISound : MonoBehaviour
{
    /// <summary>
    /// Plays the click sounds.
    /// </summary>
    /// <param name="soundClip">The click sound to play.</param>
    public void ClickSound(AudioClip soundClip)
    {
        AudioMaster.instance.PlaySpecialEffect(soundClip);
    }
}
