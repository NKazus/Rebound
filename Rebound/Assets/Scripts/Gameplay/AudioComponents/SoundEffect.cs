public abstract class SoundEffect
{
    protected SoundManager _soundManager;

    protected SoundEffect(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    public abstract void PlaySound();
}
