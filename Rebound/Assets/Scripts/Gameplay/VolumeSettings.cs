using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider globalVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle musicToggle;

    [Inject] private SoundManager _soundManager;
    [Inject] private GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        SetGlobalVolume(PlayerPrefs.GetFloat("_GlobalVolume"));
        SetEffectsVolume(PlayerPrefs.GetFloat("_EffectsVolume"));
        TurnMisic(PlayerPrefs.GetInt("_MusicEnabled") == 1);
    }

    private void OnEnable()
    {
        globalVolumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicToggle.onValueChanged.AddListener(TurnMisic);
        _eventManager.PauseEvent += Pause;
    }

    private void OnDisable()
    {
        globalVolumeSlider.onValueChanged.RemoveListener(SetGlobalVolume);
        effectsVolumeSlider.onValueChanged.RemoveListener(SetEffectsVolume);
        musicToggle.onValueChanged.RemoveListener(TurnMisic);
        _eventManager.PauseEvent -= Pause;
    }
    #endregion

    private void Pause(bool isResumed)
    {
        if (isResumed)
        {
            PlayerPrefs.SetFloat("_GlobalVolume", globalVolumeSlider.value);
            PlayerPrefs.SetFloat("_EffectsVolume", effectsVolumeSlider.value);
            PlayerPrefs.SetInt("_MusicEnabled", musicToggle.isOn ? 1 : 0);
        }
        else
        {
            globalVolumeSlider.value = PlayerPrefs.GetFloat("_GlobalVolume");
            effectsVolumeSlider.value = PlayerPrefs.GetFloat("_EffectsVolume");
            musicToggle.isOn = PlayerPrefs.GetInt("_MusicEnabled") == 1;
        }
    }

    private void SetGlobalVolume(float volumeValue)
    {
        _soundManager.SetGlobalVolume(volumeValue);
    }

    private void SetEffectsVolume(float volumeValue)
    {
        _soundManager.SetEffectsVolume(volumeValue);
    }

    private void TurnMisic(bool isMusicOn)
    {
        _soundManager.TurnMisic(isMusicOn);
    }
}
