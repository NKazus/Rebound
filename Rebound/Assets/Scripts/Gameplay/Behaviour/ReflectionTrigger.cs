using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class ReflectionTrigger : MonoBehaviour
{
    private Transform _transform;
    private ObjectColor _triggerColor;
    private float _activationTime = 1f;
    private Tween _shakeScaleTween;

    [Inject] private readonly PoolManager _pool;
    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        _transform = transform;
        _triggerColor = GetComponent<ObjectColor>();
        _shakeScaleTween = _transform.DOShakeScale(0.5f, 0.5f, 10, 50).SetLink(gameObject).OnComplete(() => _pool.PutGameObjectToPool(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _eventManager.UpdateReflection(_activationTime, _triggerColor.GetColor());
            _shakeScaleTween.Play();
        }
    }
    #endregion

    public void SetTriggerParameters(float duration, float colorValue)
    {
        _activationTime = duration;
        _triggerColor.SetColor(colorValue);
    }
}
