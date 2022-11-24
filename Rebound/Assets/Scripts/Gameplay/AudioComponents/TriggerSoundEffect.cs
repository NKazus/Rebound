public class TriggerSoundEffect : SoundEffect
{
    private float _volumeValue;

    public TriggerSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayTrigger(_volumeValue);
    }

    public override void Setup(float value)
    {
        _volumeValue = value;
    }
}
