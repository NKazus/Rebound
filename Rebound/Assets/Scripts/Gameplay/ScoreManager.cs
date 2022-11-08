using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameMessage))]
public class ScoreManager : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    [SerializeField] private GameObject scorePanel; 
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUIConst;

    private GameMessage _gameMessage;
    private float _scorePerSecond;
    private float _scoreCount;
    private float _highScoreCount;


    private void Awake()
    {
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
            ChangeScoreColor();
        }
    }

    private void LocalFixedUpdateHighScore()
    {
        _highScoreCount += _scorePerSecond * Time.fixedDeltaTime;
        scoreUI.text = highScoreUI.text = ((int)_highScoreCount).ToString();
    }

    private void ChangeScoreColor()
    {
        DOTween.Sequence().SetId(this)
            .Append(highScoreUI.DOScale(1.5f, 0.3f))
            .Join(highScoreUI.DOColor(scoreUI.color, 0.3f))
            .Append(highScoreUI.DOScale(1f, 0.3f));
        DOTween.Sequence().SetId(this)
            .Append(highScoreUIConst.DOScale(1.5f, 0.3f))
            .Join(highScoreUIConst.DOColor(scoreUI.color, 0.3f))
            .Append(highScoreUIConst.DOScale(1f, 0.3f));
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
        DOTween.Kill(this);
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _scorePerSecond = scrollSpeed;
    }
}
