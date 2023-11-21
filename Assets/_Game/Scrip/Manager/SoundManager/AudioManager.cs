using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] musicSounds, sfxSounds ;
    public AudioClip[] stepSounds;
    public AudioSource musicSource, sfxSource;
    private void Start()
    {
    }
    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
    }
    public void PlaySfx(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sfxSource.PlayOneShot(s.clip);
    }
    public void StopSfx(string name)
    {
        Sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sfxSource.Stop();
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void PlayeStepSound(){
        int i = Random.Range(0, 5);
        sfxSource.PlayOneShot(stepSounds[i]);
    }
}
