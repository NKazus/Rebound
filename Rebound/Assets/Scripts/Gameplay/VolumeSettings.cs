using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider globalVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Toggle musicToggle;

    [Inject] private readonly SoundManager _soundManager;

    #region MONO
    private void OnEnable()
    {
        globalVolumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        effectsVolumeSlider.onValueChanged.AddListener(SetEffectsVolume);
        musicToggle.onValueChanged.AddListener(TurnMisic);

        if (!PlayerPrefs.HasKey("_GlobalVolume"))
        {
            PlayerPrefs.SetFloat("_GlobalVolume", 1f);
            PlayerPrefs.SetFloat("_EffectsVolume", 1f);
            PlayerPrefs.SetInt("_MusicEnabled", 1);
        }
        globalVolumeSlider.value = PlayerPrefs.GetFloat("_GlobalVolume");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("_EffectsVolume");
        musicToggle.isOn = PlayerPrefs.GetInt("_MusicEnabled") == 1;
    }

    private void OnDisable()
    {
        globalVolumeSlider.onValueChanged.RemoveListener(SetGlobalVolume);
        effectsVolumeSlider.onValueChanged.RemoveListener(SetEffectsVolume);
        musicToggle.onValueChanged.RemoveListener(TurnMisic);

        PlayerPrefs.SetFloat("_GlobalVolume", globalVolumeSlider.value);
        PlayerPrefs.SetFloat("_EffectsVolume", effectsVolumeSlider.value);
        PlayerPrefs.SetInt("_MusicEnabled", musicToggle.isOn ? 1 : 0);
    }
    #endregion

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
