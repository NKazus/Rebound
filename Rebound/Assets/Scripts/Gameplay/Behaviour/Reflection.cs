using UnityEngine;

public class Reflection : MonoBehaviour
{
    //ссылка на компонент анимации
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            GlobalEventManager.CalculateSpeed(ReflectionController.ReflectionState.Reflect, true);
            //Color color = ReflectionController.ReflectionState.ReflectionColor;
        }
    }

}
