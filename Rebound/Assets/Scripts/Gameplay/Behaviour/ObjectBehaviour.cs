using UnityEngine;
using Zenject;

public abstract class ObjectBehaviour : MonoBehaviour
{
    protected ObjectColor _objectColor;
    protected SoundEffect _soundEffect;
    protected SoundType _soundType;

    [Inject] protected readonly GlobalEventManager _eventManager;
    [Inject] protected readonly SoundProvider _soundProvider;

    protected virtual void Awake()
    {
        _objectColor = GetComponent<ObjectColor>();
        _soundEffect = _soundProvider.GetSoundEffect(_soundType);
    }
}
