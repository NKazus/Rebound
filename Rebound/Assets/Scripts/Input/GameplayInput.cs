using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameplayInput : MonoBehaviour
{
    public event Action<Vector3> PlayerInputEvent;

    private Vector3 _playerTouch;
    private Camera _mainCamera;
    [Inject] private GlobalUpdateManager _updateManager;
    [Inject] private GlobalEventManager _eventManager;

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
        _updateManager.GlobalUpdateEvent -= LocalUpdateReload;
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
            _updateManager.GlobalUpdateEvent += LocalUpdateReload;
        }
    }

    private void LocalUpdate()
    {
        if (Input.GetMouseButtonDown(0))//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _playerTouch = _mainCamera.ScreenToWorldPoint(Input.mousePosition/*Input.GetTouch(0).position*/);
            _playerTouch.z = 0f;
            PlayerInputEvent?.Invoke(_playerTouch);
        }
    }

    private void LocalUpdateReload()
    {
        if(Input.GetMouseButtonDown(0))//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
