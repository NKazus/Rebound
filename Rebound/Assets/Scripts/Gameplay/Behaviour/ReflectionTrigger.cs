using DG.Tweening;
using UnityEngine;
using UnityEngine.Android;

public class ReflectionTrigger : MonoBehaviour
{
    private Transform _transform;
    private ObjectColor _triggerColor;
    private float _activationTime = 1f;

    private void Awake()
    {
        _transform = transform;
        _triggerColor = GetComponent<ObjectColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEventManager.UpdateReflection(_activationTime, _triggerColor.GetColor());
            _transform.DOShakeScale(0.5f, 0.5f, 10, 50).OnComplete(() => PoolManager.PutGameObjectToPool(gameObject));
        }
    }
    
    public void SetTriggerParameters(float duration, float colorValue)
    {
        _activationTime = duration;
        _triggerColor.SetColor(colorValue);
    }

}
