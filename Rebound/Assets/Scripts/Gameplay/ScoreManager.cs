using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameMessage))]
public class ScoreManager : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    [SerializeField] private GameObject scorePanel; 
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;

    private GameMessage _gameMessage;
    private float _scorePerSecond;
    private float _scoreCount;
    private float _highScoreCount;


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        _gameMessage = GetComponent<GameMessage>();
        _highScoreCount = CheckHighScore();
        highScoreUI.text = ((int)_highScoreCount).ToString();
    }

    private void OnEnable()
    { 
        GlobalEventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
        GlobalEventManager.GameStateEvent += ChangeScoreState;
    }

    private void LocalFixedUpdateScore()
    {
        _scoreCount += _scorePerSecond * Time.fixedDeltaTime;
        scoreUI.text = ((int)_scoreCount).ToString();
        if (_scoreCount > _highScoreCount)
        {
            _highScoreCount = _scoreCount;
            GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateScore;
            GlobalUpdateManager.GlobalFixedUpdateEvent += LocalFixedUpdateHighScore;
        }
    }

    private void LocalFixedUpdateHighScore()
    {
        _highScoreCount += _scorePerSecond * Time.fixedDeltaTime;
        scoreUI.text = highScoreUI.text = ((int)_highScoreCount).ToString();
    }

    private void ChangeScoreState(bool isActive)
    {
        if (isActive)
        {
            scorePanel.SetActive(true);
            GlobalUpdateManager.GlobalFixedUpdateEvent += LocalFixedUpdateScore;
        }
        else
        {
            GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateScore;
            GlobalUpdateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateHighScore;
            GlobalEventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
            SaveHighScore();
        }
    }

    private int CheckHighScore()
    {
        if (PlayerPrefs.HasKey("_HighScore"))
        {
            return PlayerPrefs.GetInt("_HighScore");
            
        }
        else
        {
            PlayerPrefs.SetInt("_HighScore", 0);
            return 0;
        }
    }

    private void SaveHighScore()
    {
        if (PlayerPrefs.GetInt("_HighScore") < _highScoreCount)
        {
            PlayerPrefs.SetInt("_HighScore", (int)_highScoreCount);
            _gameMessage.ShowNewScoreMessage();
        }
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
