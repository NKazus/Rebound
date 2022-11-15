using System;
using UnityEngine;
using Zenject;

public class GameplayInput : MonoBehaviour
{
    public event Action<Vector3> PlayerInputEvent;

    private Vector2 _playerTouch;
    private Camera _mainCamera;

    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly InputValidator _inputValidator;

    #region MONO
    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _eventManager.GameStateEvent += ChangeInputState;
    }
    private void OnDisable()
    {
        _eventManager.GameStateEvent -= ChangeInputState;
        _updateManager.GlobalUpdateEvent -= LocalUpdate;
    }
    #endregion

    private void ChangeInputState(bool isActive)
    {
        if (isActive)
        {
            _updateManager.GlobalUpdateEvent += LocalUpdate;
        }
        else
        {
            _updateManager.GlobalUpdateEvent -= LocalUpdate;
        }
    }

#if UNITY_EDITOR
    private void LocalUpdate()
    {
        if (Input.GetMouseButtonDown(0) && _inputValidator.ValidateGameplayInput(Input.mousePosition))
        {
            _playerTouch = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            PlayerInputEvent?.Invoke(_playerTouch);
        }
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    private void LocalUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _inputValidator.ValidateGameplayInput(Input.GetTouch(0).position))
        {
            _playerTouch = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            PlayerInputEvent?.Invoke(_playerTouch);
        }
    }
#endif
}
