using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] _audioSources;
    private AudioSource _ambientMusic;
    private AudioSource _reflectionSound;
    private AudioSource _refractionSound;
    private AudioSource _triggerSound;

    private void Awake()
    {
        _audioSources = GetComponents<AudioSource>();
        foreach(AudioSource audio in _audioSources)
        {
            switch (audio.clip.name)
            {
                case "ambient": _ambientMusic = audio; _ambientMusic.loop = true; break;
                case "refraction": _refractionSound = audio; break;
                case "reflection": _reflectionSound = audio; break;
                case "trigger": _triggerSound = audio; break;
                default: throw new NotSupportedException(); 
            }
        }
    }

    private void Play(AudioSource audio)
    {
        if (audio != null)
        {
            audio.Play();
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public void PlayRefraction()
    {
        Play(_refractionSound);
    }

    public void PlayReflection()
    {
        Play(_reflectionSound);
    }

    public void PlayTrigger()
    {
        Play(_triggerSound);
    }
}
