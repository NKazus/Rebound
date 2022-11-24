public class ReflectionSoundEffect : SoundEffect
{
    public ReflectionSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayReflection(BaseReflection.IsActiveState);
    }

    public override void Setup(float value)
    {
        throw new System.NotImplementedException();
    }
}
