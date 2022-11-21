using System;
using Zenject;

public enum SoundType
{
    Refraction,
    Reflection,
    Trigger
}

public class SoundProvider
{
    [Inject] private readonly SoundManager _soundManager;

    public SoundEffect GetSoundEffect(SoundType type)
    {
        return type switch
        {
            SoundType.Refraction => new RefractionSoundEffect(_soundManager),
            SoundType.Reflection => new ReflectionSoundEffect(_soundManager),
            SoundType.Trigger => new TriggerSoundEffect(_soundManager),
            _ => throw new NotSupportedException()
        };
    }
}
