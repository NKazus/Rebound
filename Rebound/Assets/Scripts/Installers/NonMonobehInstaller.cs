using Zenject;

public class NonMonobehInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        EventManagerInstaller.Install(Container);
        PoolInstaller.Install(Container);
        RefractionProviderInstaller.Install(Container);
        SoundProviderInstaller.Install(Container);
    }
}