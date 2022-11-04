using UnityEngine;

public class ReflectionTrigger : MonoBehaviour
{
    private ObjectColor _triggerColor;
    private float _activationTime = 1f;

    private void Awake()
    {
        _triggerColor = GetComponent<ObjectColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEventManager.UpdateReflection(_activationTime, _triggerColor.GetColor());
            //интенсивность и анимация исчезновения
            PoolManager.PutGameObjectToPool(gameObject);
        }
    }
    
    public void SetTriggerParameters(float duration, float colorValue)
    {
        _activationTime = duration;
        _triggerColor.SetColor(colorValue);
    }

}
