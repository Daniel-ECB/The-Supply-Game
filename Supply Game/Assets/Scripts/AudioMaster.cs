using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    #region Fields

    public static AudioMaster instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    #endregion

    #region Properties

    /// <summary>
    /// Gets and sets the current music audioclip to play.
    /// </summary>
    public AudioClip CurrentMusicClip
    {
        get
        {
            return musicSource.clip;
        }
        set
        {
            // Only start playing again if it's a different track
            if (value == musicSource.clip)
            {
                //Debug.Log("La música es la misma, continuará reproduciéndose la misma pista");
                return;
            }

            musicSource.Stop();
            musicSource.clip = value;
            musicSource.Play();
        }
    }

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Plays the given audio clip.
    /// </summary>
    /// <param name="sfxClip">The special effects audio clip to play.</param>
    public void PlaySpecialEffect(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.Play();
    }

    #endregion
}
