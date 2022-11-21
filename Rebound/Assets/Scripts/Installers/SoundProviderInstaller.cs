using Zenject;

public class SoundProviderInstaller : Installer<SoundProviderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<SoundProvider>().AsSingle();
    }
}