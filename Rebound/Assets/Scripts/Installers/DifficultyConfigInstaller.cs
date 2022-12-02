using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DifficultyConfigInstaller", menuName = "Installers/DifficultyConfigInstaller")]
public class DifficultyConfigInstaller : ScriptableObjectInstaller<DifficultyConfigInstaller>
{
    [SerializeField] private DifficultyConfig[] configs;
    public override void InstallBindings()
    {
        if (configs == null)
        {
            Debug.Log("No config presets found.");
            Application.Quit();
        }

        DifficultyConfig currentConfig;
        DifficultyLevel level;
        try
        {
            if (PlayerPrefs.HasKey("_DifficultyLevel"))
            {
                level = (DifficultyLevel) PlayerPrefs.GetInt("_DifficultyLevel");
                if (((int) level) > Enum.GetNames(typeof(DifficultyLevel)).Length)
                {
                    throw new Exception();        
                }
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            Debug.Log("No saved data found. Initialize difficulty level. Value = Easy");
            level = DifficultyLevel.Easy;
            PlayerPrefs.SetInt("_DifficultyLevel", (int) level);
        }
        
        for(int i = 0; i < configs.Length; i++)
        {
            if (configs[i].DifficultyLevel == level)
            {
                currentConfig = configs[i];
                Container.Bind<DifficultyConfig>().FromScriptableObject(currentConfig).AsSingle();
                return;
            }
        }

        Container.Bind<DifficultyConfig>().FromScriptableObject(configs[0]).AsSingle();
        PlayerPrefs.SetInt("_DifficultyLevel", (int) configs[0].DifficultyLevel);
    }
}