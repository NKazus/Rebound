using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class Refraction : ObjectBehaviour
{
    [SerializeField] private RefractionType type;

    private IRefraction _refraction;

    [Inject] private readonly RefractionProvider _refractionProvider;

    #region MONO
    protected override void Awake()
    {
        _refraction = _refractionProvider.GetRefraction(type);
        _soundType = SoundType.Refraction;
        base.Awake();
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
        _soundEffect.Setup(Mathf.Clamp(colorValue, 0.5f, 1f));
    }
}

public interface IRefraction
{
    public float Refract(float value);
}
