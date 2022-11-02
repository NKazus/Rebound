using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;

    private float _scorePerSecond;
    private float _scoreCount;
    private float _highScoreCount;


    private void Awake()
    {
        CheckHighScore();
    }

    private void OnEnable()
    { 
        GlobalEventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
        GlobalEventManager.GameStateEvent += ChangeScoreState;
    }

    private void LocalFixedUpdate()
    {
        _scoreCount += _scorePerSecond * Time.fixedDeltaTime;

        if(_scoreCount > _highScoreCount)
        {
            _highScoreCount = _scoreCount;
        }
        
        scoreUI.text = ((int)_scoreCount).ToString();
        highScoreUI.text = ((int)_highScoreCount).ToString();
    }

    private void ChangeScoreState(bool isActive)
    {
        if (isActive)
        {
            GlobalUpdateManager.GlobalFixedUpdateEvent += LocalFixedUpdate;
        }
        else
        {
            GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
            GlobalEventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
            SaveHighScore();
        }
    }

    private void CheckHighScore()
    {
        _highScoreCount = 10f;
    }

    private void SaveHighScore()
    {

    }

    private void OnDisable()
    {
        GlobalEventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
        GlobalEventManager.GameStateEvent -= ChangeScoreState;
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _scorePerSecond = scrollSpeed;
    }
}
