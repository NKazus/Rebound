using System;
using UnityEngine;

public class GlobalSpeedController : MonoBehaviour
{
    public static float ScrollSpeed { get; private set; }
    
    [SerializeField] private PlayerMovement player;
    [SerializeField] private ObjectColor playerColor;

    private float _globalSpeed;
    private float _playerSpeed;
    private float _calculatedScrollSpeed;
    private float _maxScrollSpeed;
    private float _minScrollSpeed;
    private float _deltaSpeedAngle;
    private float _deltaCosine;

    private void Awake()
    {
        var configInfo = Resources.Load<InitConfig>("InitConfig");
        
        _globalSpeed = configInfo.InitialGlobalSpeed;
        _maxScrollSpeed = configInfo.MaxScrollSpeed;
        _minScrollSpeed = configInfo.MinScrollSpeed;

        _deltaSpeedAngle = configInfo.MinDeflectionAngle;
        _deltaCosine = Mathf.Cos(_deltaSpeedAngle * Mathf.Deg2Rad);
    }

    private void OnEnable()
    {
        GlobalEventManager.InitializeSpeedEvent += InitializeSpeedParameters;
        GlobalEventManager.CalculateSpeedEvent += UpdateSpeedValues;
    }


    private void InitializeSpeedParameters(Vector3 initialDirection)
    {
        player.SetInitialSpeedSign(Mathf.Sign(initialDirection.x));

        float initialAngle = Vector3.Angle(Vector3.up, initialDirection);        
        initialAngle = initialAngle < _deltaSpeedAngle ? _deltaSpeedAngle : initialAngle;  

        _playerSpeed = _globalSpeed * Mathf.Sin(initialAngle * Mathf.Deg2Rad);
        ScrollSpeed = _globalSpeed * Mathf.Cos(initialAngle * Mathf.Deg2Rad);

        ShareSpeedChanges();
        GlobalEventManager.InitializeSpeedEvent -= InitializeSpeedParameters;
    }

    private void UpdateSpeedValues(Func<float, float> calculate, bool reflect)
    {
        _calculatedScrollSpeed = calculate(ScrollSpeed);
        if (_calculatedScrollSpeed < 0)
        {
            GlobalEventManager.CalculateSpeedEvent -= UpdateSpeedValues;
            GlobalEventManager.UpdateScrollingSpeed(_minScrollSpeed);
            GlobalEventManager.EliminatePlayer();
            return;
        }
        ScrollSpeed = Mathf.Clamp(_calculatedScrollSpeed, _minScrollSpeed, _maxScrollSpeed);
        _globalSpeed = ScrollSpeed > _globalSpeed ? (ScrollSpeed / _deltaCosine) : _globalSpeed;
        RecalculatePlayerSpeed();
        if (reflect)
        {
            player.ReversePlayerSpeedSign();
        }
        ShareSpeedChanges();
    }

    private void RecalculatePlayerSpeed()
    {
        _playerSpeed = Mathf.Sqrt(_globalSpeed * _globalSpeed - ScrollSpeed * ScrollSpeed);
    }

    private void ShareSpeedChanges()
    {
        player.SetPlayerSpeedAbs(_playerSpeed);
        playerColor.SetColor(_playerSpeed/_globalSpeed);
        GlobalEventManager.UpdateScrollingSpeed(ScrollSpeed);
    }

    private void OnDisable()
    {
        GlobalEventManager.CalculateSpeedEvent -= UpdateSpeedValues;
    }

    public interface IGlobalScroll
    {
        public void SetScrollSpeed(float value);
    }
}
