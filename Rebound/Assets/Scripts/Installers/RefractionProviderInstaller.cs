using Zenject;

public class RefractionProviderInstaller : Installer<RefractionProviderInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<RefractionProvider>().AsSingle();
    }
}