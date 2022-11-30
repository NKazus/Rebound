using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;

    private bool isPaused;

    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void OnEnable()
    {
        _eventManager.GameStateEvent += ChangeState;
    }

    private void OnDisable()
    {
        _eventManager.GameStateEvent -= ChangeState;
        pauseButton.onClick.RemoveListener(PauseScene);
    }
    #endregion

    private void ChangeState(bool isActive)
    {
        pauseButton.gameObject.SetActive(isActive);
        if (isActive)
        {            
            pauseButton.onClick.AddListener(PauseScene);
        }
        else if(isPaused)
        {
            PauseScene();
        }
    }

    private void PauseScene()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        _eventManager.PauseGame(!isPaused);
    }
}
