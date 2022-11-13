using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class Refraction : MonoBehaviour
{
    [SerializeField] private RefractionType type;
    private IRefraction _refraction;
    private ObjectColor _objectColor;
    [Inject] private GlobalEventManager _eventManager;
    [Inject] private RefractionProvider _refractionProvider;

    #region MONO
    private void Awake()
    {
        _refraction = _refractionProvider.GetRefraction(type);
        _objectColor = GetComponent<ObjectColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _refraction != null)
        {
            _eventManager.CalculateSpeed(_refraction.Refract, false);
            _objectColor.ActivateGlowEffect(true);
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
