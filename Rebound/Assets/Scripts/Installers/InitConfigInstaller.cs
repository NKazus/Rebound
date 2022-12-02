using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "InitConfigInstaller", menuName = "Installers/InitConfigInstaller")]
public class InitConfigInstaller : ScriptableObjectInstaller<InitConfigInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<InitConfig>().FromScriptableObjectResource("Configs/InitConfig").AsSingle();
    }
}