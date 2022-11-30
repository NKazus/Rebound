using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Reloader : MonoBehaviour
{
    [SerializeField] private RectTransform reloadArea;
    [SerializeField] private Button restartButton;

    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly InputValidator _validator;

    #region MONO
    private void OnEnable()
    {
        _eventManager.GameStateEvent += ChangeState;
    }

    private void OnDisable()
    {
        _updateManager.GlobalUpdateEvent -= LocalUpdate;
        _eventManager.GameStateEvent -= ChangeState;
    }
    #endregion

    private void ChangeState(bool isActive)
    {
        if (isActive)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(ReloadScene);
            _eventManager.PauseEvent += Pause;
        }
        else
        {
            restartButton.onClick.RemoveListener(ReloadScene);
            restartButton.gameObject.SetActive(false);
            _updateManager.GlobalUpdateEvent += LocalUpdate;
            _eventManager.PauseEvent -= Pause;
        }
    }

    private void Pause(bool resumed)
    {
        restartButton.gameObject.SetActive(resumed);
    }

#if UNITY_EDITOR
    private void LocalUpdate()
    {
        if (Input.GetMouseButtonUp(0) && _validator.ValidateReplayInput(Input.mousePosition))
        {
            ReloadScene();
        }
    }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
    private void LocalUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && _validator.ValidateReplayInput(Input.GetTouch(0).position))
        {
            ReloadScene();
        }
    }
#endif

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
