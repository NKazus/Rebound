public class RefractionSoundEffect : SoundEffect
{
    private float _pitchValue;
    public RefractionSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayRefraction(_pitchValue);
    }

    public override void Setup(float value)
    {
        _pitchValue = value;
    }
}
