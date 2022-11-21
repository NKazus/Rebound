public class ReflectionSoundEffect : SoundEffect
{
    public ReflectionSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayReflection();
    }
}
