using UnityEngine;
using Zenject;

public class PoolInstaller : Installer<PoolInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().AsSingle();
    }
}