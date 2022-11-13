using UnityEngine;
using Zenject;

public class GameInputInstaller : MonoInstaller
{
    [SerializeField] private GameplayInput gameplayInput;
    public override void InstallBindings()
    {
        Container.Bind<GameplayInput>().FromInstance(gameplayInput).AsSingle().NonLazy();
    }
}