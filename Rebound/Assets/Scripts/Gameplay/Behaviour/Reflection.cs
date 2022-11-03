using UnityEngine;

public class Reflection : MonoBehaviour
{
    private ObjectColor _objectColor;

    private void Awake()
    {
        _objectColor = GetComponent<ObjectColor>();
    }

    private void OnEnable()
    {
        _objectColor.SetColor(ReflectionController.ReflectionState.ReflectionColor);
        ReflectionController.ReflectionUpdateEvent += UpdateReflection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GlobalEventManager.CalculateSpeed(ReflectionController.ReflectionState.Reflect, true);
            _objectColor.ActivateGlowEffect(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _objectColor.ActivateGlowEffect(false);
        }
    }

    private void UpdateReflection(Color color)
    {
        _objectColor.SetColor(color);
    }

    private void OnDisable()
    {
        ReflectionController.ReflectionUpdateEvent -= UpdateReflection;
    }
}
