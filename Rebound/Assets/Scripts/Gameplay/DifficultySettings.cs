using System;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class DifficultySettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown difficultyDropdown;

    [Inject] private readonly DifficultyConfig _difficultyConfig;

    private void OnEnable()
    {
        difficultyDropdown.AddOptions(Enum.GetNames(typeof(DifficultyLevel)).ToList());
        difficultyDropdown.value = (int) _difficultyConfig.DifficultyLevel - 1;
    }

    private void OnDisable()
    {
        if (((int) _difficultyConfig.DifficultyLevel) != difficultyDropdown.value + 1)
        {
            PlayerPrefs.SetInt("_DifficultyLevel", difficultyDropdown.value + 1);
        }
    }
}
