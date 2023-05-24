using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (AudioInstance==null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM("BGMusic");
    }

    public void PlayBGM(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound file not found");
        }
        else
        {
            musicSource.volume = 0.5f;
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlayClip(string soundToPlay, float volume)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == soundToPlay);

        if (sound == null)
        {
            Debug.Log("Sound file not found");
        }
        else
        {
            musicSource.volume = volume;
            musicSource.PlayOneShot(sound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
