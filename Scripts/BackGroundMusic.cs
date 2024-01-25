using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackGroundMusic : MonoBehaviour
{
    /// <summary>
    /// Static variable so the music doesnt stop between scenes
    /// </summary>
    private static BackGroundMusic backGroundMusic;

    /// <summary>
    /// list for all the background songs
    /// </summary>
    [SerializeField] private List<AudioClip> backgroundSounds;

    /// <summary>
    /// the 2 dropdowns variables for the dropdowns in the canvas
    /// </summary>
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private TMP_Dropdown dropDownPaused;

    /// <summary>
    /// The audiosource used to play the sounds
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    /// Make sure there is 1 
    /// </summary>
    private void Awake()
    {
        if (backGroundMusic == null)
        {
            backGroundMusic = this;
            DontDestroyOnLoad(backGroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Set the audioSource variable and add listeners to
    /// the dropdowns
    /// </summary>
    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        audioSource = GetComponent<AudioSource>();
        dropDown.onValueChanged.AddListener(delegate { soundsChanged(dropDown); });
        dropDownPaused.onValueChanged.AddListener(delegate { soundsChanged(dropDownPaused); });
    }

    /// <summary>
    /// Change the songs with the help of the value of the sender
    /// </summary>
    /// <param name="sender">The dropdown that got changed</param>
    private void soundsChanged(TMP_Dropdown sender)
    {
        if (sender.value == 0)
        {
            audioSource.clip = backgroundSounds[0];
            audioSource.Play();
        }
        else if (sender.value == 1)
        {
            audioSource.clip = backgroundSounds[1];
            audioSource.Play();
        }
        else if (sender.value == 2)
        {
            audioSource.clip = backgroundSounds[2];
            audioSource.Play();
        }
        else if (sender.value == 3)
        {
            audioSource.clip = backgroundSounds[3];
            audioSource.Play();
        }
    }
}
