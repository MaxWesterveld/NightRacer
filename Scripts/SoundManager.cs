using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Slider for the volume of the game
    /// </summary>
    [SerializeField] private Slider volumeSlider;

    /// <summary>
    /// Check if musicVolume already exists
    /// if yes load it, if not set it to 1
    /// </summary>
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Changes the volume of the gam through the slider and saves it
    /// </summary>
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    /// <summary>
    /// loads the last saved musicVolume value setting
    /// </summary>
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    /// <summary>
    /// Saves the current musicVolum value
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
