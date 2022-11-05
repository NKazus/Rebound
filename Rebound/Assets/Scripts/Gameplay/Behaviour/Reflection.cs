using DG.Tweening;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    private ObjectColor _objectColor;
    Transform _transform;

    private void Awake()
    {
        _objectColor = GetComponent<ObjectColor>();
        _transform = transform;
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
            _transform.DOPunchScale(new Vector3(0.2f, 0, 0), 0.1f, 5);
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
