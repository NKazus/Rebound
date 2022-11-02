using UnityEngine;

public class ReflectionTrigger : MonoBehaviour
{
    private ObjectColor _triggerColor;
    private float _activationTime = 1f;
    private Color _currentTriggerColor;

    private void Awake()
    {
        _triggerColor = GetComponent<ObjectColor>();
        _currentTriggerColor = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalEventManager.UpdateReflection(_activationTime, _currentTriggerColor);
            PoolManager.PutGameObjectToPool(gameObject);
        }
    }
    
    public void SetTriggerParameters(float duration, float colorValue)
    {
        _activationTime = duration;
        _currentTriggerColor = _triggerColor.SetColor(colorValue);
    }

}
