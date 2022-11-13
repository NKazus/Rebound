using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerColor playerColor;

    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle().NonLazy();
        Container.Bind<PlayerColor>().FromInstance(playerColor).AsSingle().NonLazy();
    }
}