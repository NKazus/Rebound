using System;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static event Action<Vector3> InitializeSpeedEvent;
    public static event Action<bool> GameStateEvent;
    public static event Action<float> ResetScrollingSpeedEvent;
    public static event Action<Func<float, float>, bool> CalculateSpeedEvent;
    public static event Action<float, Color> ActivateReflectionEvent;


    public static void InitializeMovement(Vector3 initialDirection)// initial move
    {
        InitializeSpeedEvent?.Invoke(initialDirection);
        //начало спавна
        GameStateEvent?.Invoke(true);
    }

    public static void UpdateScrollingSpeed(float scrollingSpeed)//speedcontroller
    {
        ResetScrollingSpeedEvent?.Invoke(scrollingSpeed);
    }

    public static void CalculateSpeed(Func<float,float> speedHandler, bool reflect)//reflection / refraction
    {
        CalculateSpeedEvent?.Invoke(speedHandler, reflect);
	//вызов ивента, на который подписаны обработчики анимации
    }

    public static void UpdateReflection(float duration, Color triggerColor)
    {
        ActivateReflectionEvent?.Invoke(duration, triggerColor);
    }

    public static void EliminatePlayer()//триггерится смертельным препятствием
    {
        //вызвать метод у спид контроллера, который отключит игрока
        //остановка спавна
        GameStateEvent?.Invoke(false);
    }
}
