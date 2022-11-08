using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ObjectColor))]
public class Reflection : MonoBehaviour
{
    private ObjectColor _objectColor;
    private Transform _transform;
    private Tween _punchScaleTween;

    private void Awake()
    {
        _objectColor = GetComponent<ObjectColor>();
        _transform = transform;
        _punchScaleTween = _transform.DOPunchScale(new Vector3(0.2f, 0, 0), 0.1f, 5).SetLink(gameObject);
    }

    private void OnEnable()
    {
        ReflectionController.ReflectionUpdateEvent += UpdateReflection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GlobalEventManager.CalculateSpeed(ReflectionController.ReflectionState.Reflect, true);            
            _objectColor.ActivateGlowEffect(ReflectionController.BaseReflection.IsActiveState);
            _punchScaleTween.Play();
        }
    }

    private void UpdateReflection(float intensity)
    {
        _objectColor.SetColor(intensity);
    }

    private void OnDisable()
    {
        ReflectionController.ReflectionUpdateEvent -= UpdateReflection;
    }
}
