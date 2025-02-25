using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider, musicSlider, sfxSlider;

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
    }
}
