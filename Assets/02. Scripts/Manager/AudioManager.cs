using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioMixer myMixer;
    public AudioSource musicSource, sfxSource;

    public float MasterValue { get; private set; }
    public float MusicValue { get; private set; }
    public float SFXValue { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic("Background");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData(); // Load
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            MasterValue = PlayerPrefs.GetFloat("MasterVolume");
            myMixer.SetFloat("master", Mathf.Log10(MasterValue) * 20);
        }
        else
        {
            MasterValue = 0.5f;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            MusicValue = PlayerPrefs.GetFloat("MusicVolume");
            myMixer.SetFloat("music", Mathf.Log10(MusicValue) * 20);
        }
        else
        {
            MusicValue = 0.5f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXValue = PlayerPrefs.GetFloat("SFXVolume");
            myMixer.SetFloat("sfx", Mathf.Log10(PlayerPrefs.GetFloat("SFXValue")) * 20);
        }
        else
        {
            SFXValue = 0.5f;
        }
    }
}
