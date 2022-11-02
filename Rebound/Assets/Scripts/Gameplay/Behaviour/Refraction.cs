using UnityEngine;

public class Refraction : MonoBehaviour
{
    private IRefraction _refraction;
    private ObjectColor _objectColor;
    //ссылка на компонент визуала с анимацией

    private void Awake()
    {
        _refraction = GetComponent<IRefraction>();
        _objectColor = GetComponent<ObjectColor>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _refraction != null)
        {
            GlobalEventManager.CalculateSpeed(_refraction.Refract, false);
            //вызов анимации
        }
    }

    public void SetObjectParameters(float colorValue)
    {
        _objectColor.SetColor(colorValue);
    }

    public interface IRefraction
    {
        public float Refract(float value);
    }
}
