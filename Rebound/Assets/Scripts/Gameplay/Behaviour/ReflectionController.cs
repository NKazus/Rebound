using UnityEngine;

public class ReflectionController : MonoBehaviour
{
    public static BaseReflection ReflectionState { get; private set; } = new SimpleReflection(Color.white);

    private float _remainingTime = 0f;


    private void OnEnable()
    {
        GlobalEventManager.ActivateReflectionEvent += ActivateReflection;
    }

    private void LocalFixedUpdate()
    {
        _remainingTime -= Time.fixedDeltaTime;
        if (_remainingTime < 0f)
        {
            ReflectionState = ReflectionState.SwitchMode(Color.white);
            GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
        }     
    }

    private void ActivateReflection(float activationTime, Color triggerColor)
    {
        if (!BaseReflection.IsActiveState)
        {
            ReflectionState = ReflectionState.SwitchMode(triggerColor);
            GlobalUpdateManager.GlobalFixedUpdateEvent += LocalFixedUpdate;
        }
        _remainingTime = _remainingTime > activationTime ? _remainingTime : activationTime;
    }

    private void OnDisable()
    {
        GlobalEventManager.ActivateReflectionEvent -= ActivateReflection;
        GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
    }


    public abstract class BaseReflection
    {
        public static bool IsActiveState { get; protected set; }
        public Color ReflectionColor { get; protected set; }    
        public abstract float Reflect(float value);
        public abstract BaseReflection SwitchMode(Color value);
    }

    public class ReducingReflection : BaseReflection
    {
        public ReducingReflection(Color initialColor) { IsActiveState = true; ReflectionColor = initialColor; }
        public override float Reflect(float value) { return value -= value * 0.05f; }
        public override BaseReflection SwitchMode(Color value) { return new SimpleReflection(value); }
    }

    public class SimpleReflection : BaseReflection
    {
        public SimpleReflection(Color initialColor) { IsActiveState = false; ReflectionColor = initialColor; }
        public override float Reflect(float value) { return value; }
        public override BaseReflection SwitchMode(Color value) { return new ReducingReflection(value); }
    }
}
