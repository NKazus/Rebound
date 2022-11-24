using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class Reflection : ObjectBehaviour
{
    private Transform _transform;
    private Tween _punchScaleTween;

    [Inject] private readonly ReflectionController _controller;

    #region MONO
    protected override void Awake()
    {
        _transform = transform;
        _punchScaleTween = _transform.DOPunchScale(new Vector3(0.2f, 0, 0), 0.1f, 5).SetLink(gameObject);
        _soundType = SoundType.Reflection;
        base.Awake();
    }

    private void OnEnable()
    {
        _controller.ReflectionUpdateEvent += UpdateReflection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _eventManager.CalculateSpeed(_controller.ReflectionState.Reflect, true);            
            _objectColor.ActivateGlowEffect(BaseReflection.IsActiveState);
            _punchScaleTween.Play();
            _soundEffect.PlaySound();
        }
    }
    private void OnDisable()
    {
        _controller.ReflectionUpdateEvent -= UpdateReflection;
    }
    #endregion

    private void UpdateReflection(float intensity)
    {
        _objectColor.SetColor(intensity);
    }
}
