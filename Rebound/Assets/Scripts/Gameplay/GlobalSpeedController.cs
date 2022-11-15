using System;
using UnityEngine;
using Zenject;

public class GlobalSpeedController : MonoBehaviour
{
    public static float ScrollSpeed { get; private set; }

    private float _globalSpeed;
    private float _playerSpeed;
    private float _calculatedScrollSpeed;
    private float _maxScrollSpeed;
    private float _minScrollSpeed;
    private float _deltaSpeedAngle;
    private float _deltaCosine;

    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerColor _playerColor;
    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly InitConfig _initConfig;

    #region MONO
    private void Awake()
    {
        _globalSpeed = _initConfig.InitialGlobalSpeed;
        _maxScrollSpeed = _initConfig.MaxScrollSpeed;
        _minScrollSpeed = _globalSpeed * Mathf.Cos(_initConfig.MaxInitialAngle * Mathf.Deg2Rad);

        _deltaSpeedAngle = _initConfig.MinDeflectionAngle;
        _deltaCosine = Mathf.Cos(_deltaSpeedAngle * Mathf.Deg2Rad);
    }

    private void OnEnable()
    {
        _eventManager.InitializeSpeedEvent += InitializeSpeedParameters;
        _eventManager.CalculateSpeedEvent += UpdateSpeedValues;
    }
    private void OnDisable()
    {
        _eventManager.CalculateSpeedEvent -= UpdateSpeedValues;
    }
    #endregion

    private void InitializeSpeedParameters(Vector2 initialDirection)
    {
        _playerMovement.SetInitialSpeedSign(Mathf.Sign(initialDirection.x));

        float initialAngle = Vector2.Angle(Vector2.up, initialDirection);        
        initialAngle = initialAngle < _deltaSpeedAngle ? _deltaSpeedAngle : initialAngle;  

        _playerSpeed = _globalSpeed * Mathf.Sin(initialAngle * Mathf.Deg2Rad);
        ScrollSpeed = _globalSpeed * Mathf.Cos(initialAngle * Mathf.Deg2Rad);

        ShareSpeedChanges();
        _eventManager.InitializeSpeedEvent -= InitializeSpeedParameters;
    }

    private void UpdateSpeedValues(Func<float, float> calculate, bool reflect)
    {
        _calculatedScrollSpeed = calculate(ScrollSpeed);
        if (_calculatedScrollSpeed < 0)
        {
            _eventManager.CalculateSpeedEvent -= UpdateSpeedValues;
            _eventManager.UpdateScrollingSpeed(_minScrollSpeed);
            _eventManager.EliminatePlayer();
            return;
        }
        ScrollSpeed = Mathf.Clamp(_calculatedScrollSpeed, _minScrollSpeed, _maxScrollSpeed);
        if (reflect)
        {
            _playerMovement.ReversePlayerSpeedSign();
        }
        else
        { 
            _globalSpeed = ScrollSpeed > _globalSpeed ? (ScrollSpeed / _deltaCosine) : _globalSpeed;
        }
        RecalculatePlayerSpeed();
        ShareSpeedChanges();
    }

    private void RecalculatePlayerSpeed()
    {
        _playerSpeed = Mathf.Sqrt(_globalSpeed * _globalSpeed - ScrollSpeed * ScrollSpeed);
    }

    private void ShareSpeedChanges()
    {
        _playerMovement.SetPlayerSpeedAbs(_playerSpeed);
        _playerColor.SetColor(_playerSpeed/_globalSpeed);
        _eventManager.UpdateScrollingSpeed(ScrollSpeed);
    }
}

public interface IGlobalScroll
{
    public void SetScrollSpeed(float value);
}