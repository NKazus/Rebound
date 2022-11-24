using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class ReflectionTrigger : ObjectBehaviour
{
    private Transform _transform;
    private float _activationTime = 1f;
    private Tween _shakeScaleTween;

    [Inject] private readonly PoolManager _pool;

    #region MONO
    protected override void Awake()
    {
        _transform = transform;
        _shakeScaleTween = _transform.DOShakeScale(0.5f, 0.5f, 10, 50).SetLink(gameObject).OnComplete(() => _pool.PutGameObjectToPool(gameObject));
        _soundType = SoundType.Trigger;
        base.Awake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _eventManager.UpdateReflection(_activationTime, _objectColor.GetColor());
            _shakeScaleTween.Play();
            _soundEffect.PlaySound();
        }
    }
    #endregion

    public void SetTriggerParameters(float duration, float colorValue)
    {
        _activationTime = duration;
        _objectColor.SetColor(colorValue);
        _soundEffect.Setup(Mathf.Clamp(colorValue, 0.5f, 1f));
    }
}
