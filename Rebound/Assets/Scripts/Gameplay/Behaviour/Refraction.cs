using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class Refraction : MonoBehaviour
{
    [SerializeField] private RefractionType type;

    private IRefraction _refraction;
    private ObjectColor _objectColor;
    private  SoundEffect _soundEffect;

    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly RefractionProvider _refractionProvider;
    [Inject] private readonly SoundProvider _soundProvider;

    #region MONO
    private void Awake()
    {
        _refraction = _refractionProvider.GetRefraction(type);
        _objectColor = GetComponent<ObjectColor>();
        _soundEffect = _soundProvider.GetSoundEffect(SoundType.Refraction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _refraction != null)
        {
            _eventManager.CalculateSpeed(_refraction.Refract, false);
            _objectColor.ActivateGlowEffect(true);
            _soundEffect.PlaySound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _objectColor.ActivateGlowEffect(false);
        }
    }
    #endregion

    public void SetObjectParameters(float colorValue)
    {
        _objectColor.SetColor(colorValue);
    }
}

public interface IRefraction
{
    public float Refract(float value);
}
