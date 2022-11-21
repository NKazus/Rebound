public class RefractionSoundEffect : SoundEffect
{
    public RefractionSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayRefraction();
    }
}
