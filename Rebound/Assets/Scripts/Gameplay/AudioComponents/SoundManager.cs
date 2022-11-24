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
                case string clipName when clipName.Contains("ambient"): _ambientMusic = audio; _ambientMusic.loop = true; break;
                case string clipName when clipName.Contains("refraction"): _refractionSound = audio; break;
                case string clipName when clipName.Contains("reflection"): _reflectionSound = audio; break;
                case string clipName when clipName.Contains("trigger"): _triggerSound = audio; break;
                default: throw new NotSupportedException(); 
            }
        }
    }

    private void PlaySource(AudioSource audio)
    {
        audio.Play();
    }

    private void ValidateSource(AudioSource audio)
    {
        if(audio == null)
        {
            throw new NotImplementedException();
        }
    }

    public void PlayRefraction(float pitchValue)
    {
        try
        {
            ValidateSource(_refractionSound);
            _refractionSound.pitch = pitchValue;
            PlaySource(_refractionSound);
        }
        catch (NotImplementedException exception)
        {
            Debug.Log("Refraction AudioSource: " + exception);
        }
    }

    public void PlayReflection(bool isActive)
    {
        try
        {
            ValidateSource(_reflectionSound);
            _reflectionSound.pitch = isActive ? 0.7f : 0.9f;
            PlaySource(_reflectionSound);
        }
        catch(NotImplementedException exception)
        {
            Debug.Log("Reflection AudioSource: " + exception);
        }
    }

    public void PlayTrigger(float volumeValue)
    {
        try
        {
            ValidateSource(_triggerSound);
            _triggerSound.volume = volumeValue;
            PlaySource(_triggerSound);
        }
        catch (NotImplementedException exception)
        {
            Debug.Log("Trigger AudioSource: " + exception);
        }       
    }
}
