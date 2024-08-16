using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetAll : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            SetMusicVolume();
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            SetSFXVlolume();
        }
    }
    public void SetMusicVolume()
    {
        float volume = PlayerPrefs.GetFloat("musicVolume");
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    public void SetSFXVlolume()
    {
        float volume = PlayerPrefs.GetFloat("SFX");
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
