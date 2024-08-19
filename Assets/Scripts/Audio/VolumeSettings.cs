using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            LoadSFX();
        }
        else
        {
            SetSFXVolume();
        }
       
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }
    public void SetSFXVolume()
    {
        float volume=SFXSlider.value;
        Debug.Log(volume);
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFX", volume);
    }
    private void LoadSFX()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFX");
        SetSFXVolume();
    }
}
