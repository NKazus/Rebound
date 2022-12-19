using System;
using UnityEngine;
using Zenject;

public class ReflectionController : MonoBehaviour
{
    public BaseReflection ReflectionState { get; private set; } = new SimpleReflection(Color.white);
    public event Action<float> ReflectionUpdateEvent;

    private float _remainingTime = 0f;

    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void OnEnable()
    {
        _eventManager.ActivateReflectionEvent += ActivateReflection;
    }
    private void OnDisable()
    {
        _eventManager.ActivateReflectionEvent -= ActivateReflection;
        _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
    }
    #endregion

    private void LocalFixedUpdate()
    {
        _remainingTime -= Time.fixedDeltaTime;
        if (_remainingTime < 0f)
        {
            ReflectionState = ReflectionState.SwitchMode(Color.white);
            ReflectionUpdateEvent?.Invoke(1f);
            _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
        }     
    }

    private void ActivateReflection(float activationTime, Color triggerColor)
    {
        if (!BaseReflection.IsActiveState)
        {
            ReflectionState = ReflectionState.SwitchMode(triggerColor);
            ReflectionUpdateEvent?.Invoke(activationTime);
            _updateManager.GlobalFixedUpdateEvent += LocalFixedUpdate;
        }
        _remainingTime = _remainingTime > activationTime ? _remainingTime : activationTime;
    }

    [Inject] public void SetupReflection(DifficultyConfig difficultyConfig)
    {
        ReducingReflection.SetCoefficient(difficultyConfig.ReducingCoefficient);
    }
}

#region ReflectionMode
public abstract class BaseReflection
{
    public static bool IsActiveState { get; protected set; }
    public Color ReflectionColor { get; protected set; }
    public abstract float Reflect(float value);
    public abstract BaseReflection SwitchMode(Color value);
}

public class ReducingReflection : BaseReflection
{
    private static float _reducingCoefficient = 0.05f;
    public ReducingReflection(Color initialColor) { IsActiveState = true; ReflectionColor = initialColor; }
    public override float Reflect(float value) { return value -= value * _reducingCoefficient; }
    public override BaseReflection SwitchMode(Color value) { return new SimpleReflection(value); }
    public static void SetCoefficient(float value) { _reducingCoefficient = value; }
}

public class SimpleReflection : BaseReflection
{
    public SimpleReflection(Color initialColor) { IsActiveState = false; ReflectionColor = initialColor; }
    public override float Reflect(float value) { return value; }
    public override BaseReflection SwitchMode(Color value) { return new ReducingReflection(value); }
}
#endregion