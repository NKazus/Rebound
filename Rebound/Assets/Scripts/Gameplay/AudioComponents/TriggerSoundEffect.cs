public class TriggerSoundEffect : SoundEffect
{
    public TriggerSoundEffect(SoundManager soundManager) : base(soundManager) { }

    public override void PlaySound()
    {
        _soundManager.PlayTrigger();
    }
}
