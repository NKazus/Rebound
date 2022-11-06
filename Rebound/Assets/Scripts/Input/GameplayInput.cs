using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayInput : MonoBehaviour
{
    public static event Action<Vector3> PlayerInputEvent;

    private Vector3 _playerTouch;
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        GlobalEventManager.GameStateEvent += ChangeInputState;
    }

    private void ChangeInputState(bool isActive)
    {
        if (isActive)
        {
            GlobalUpdateManager.GlobalUpdateEvent += LocalUpdate;
        }
        else
        {
            GlobalUpdateManager.GlobalUpdateEvent -= LocalUpdate;
            GlobalUpdateManager.GlobalUpdateEvent += LocalUpdateReload;
        }
    }


    private void LocalUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerTouch = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _playerTouch.z = 0f;
            PlayerInputEvent?.Invoke(_playerTouch);
        }
    }

    private void LocalUpdateReload()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnDisable()
    {
        GlobalEventManager.GameStateEvent -= ChangeInputState;
        GlobalUpdateManager.GlobalUpdateEvent -= LocalUpdateReload;
    }
}
