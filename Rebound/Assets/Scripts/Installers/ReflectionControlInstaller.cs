using UnityEngine;
using Zenject;

public class ReflectionControlInstaller : MonoInstaller
{
    [SerializeField] private ReflectionController controller;
    public override void InstallBindings()
    {
        Container.Bind<ReflectionController>().FromInstance(controller).AsSingle().NonLazy();
    }
}