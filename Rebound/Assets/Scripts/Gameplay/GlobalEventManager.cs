using System;
using UnityEngine;

public class GlobalEventManager
{
    public event Action<Vector2> InitializeSpeedEvent;
    public event Action<bool> GameStateEvent;
    public event Action<float> ResetScrollingSpeedEvent;
    public event Action<Func<float, float>, bool> CalculateSpeedEvent;
    public event Action<float, Color> ActivateReflectionEvent;
    public event Action<bool> PauseEvent;

    public void InitializeMovement(Vector2 initialDirection)// initial move
    {
        InitializeSpeedEvent?.Invoke(initialDirection);
        GameStateEvent?.Invoke(true);
    }

    public void UpdateScrollingSpeed(float scrollingSpeed)//speedcontroller
    {
        ResetScrollingSpeedEvent?.Invoke(scrollingSpeed);
    }

    public void CalculateSpeed(Func<float,float> speedHandler, bool reflect)//reflection / refraction
    {
        CalculateSpeedEvent?.Invoke(speedHandler, reflect);
    }

    public void UpdateReflection(float duration, Color triggerColor)//reflection trigger
    {
        ActivateReflectionEvent?.Invoke(duration, triggerColor);
    }

    public void EliminatePlayer()//speed controller
    {
        GameStateEvent?.Invoke(false);
    }

    public void PauseGame(bool isResumed)//pause menu
    {
        PauseEvent?.Invoke(isResumed);
    }
}
