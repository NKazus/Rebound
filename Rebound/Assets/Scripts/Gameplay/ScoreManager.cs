using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(GameMessage))]
public class ScoreManager : MonoBehaviour, IGlobalScroll
{
    [SerializeField] private GameObject scorePanel; 
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUIConst;

    private GameMessage _gameMessage;
    private float _scorePerSecond;
    private float _scoreCount;
    private float _highScoreCount;

    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        _gameMessage = GetComponent<GameMessage>();
        _highScoreCount = CheckHighScore();
        highScoreUI.text = ((int)_highScoreCount).ToString();
    }

    private void OnEnable()
    {
        _eventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
        _eventManager.GameStateEvent += ChangeScoreState;
        _eventManager.PauseEvent += Pause;
    }
    private void OnDisable()
    {
        _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
        _eventManager.GameStateEvent -= ChangeScoreState;
        _eventManager.PauseEvent -= Pause;
        DOTween.Kill(this);
    }
    #endregion

    private void LocalFixedUpdateScore()
    {
        _scoreCount += _scorePerSecond * Time.fixedDeltaTime;
        scoreUI.text = ((int)_scoreCount).ToString();
        if (_scoreCount > _highScoreCount)
        {
            _highScoreCount = _scoreCount;
            _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateScore;
            _updateManager.GlobalFixedUpdateEvent += LocalFixedUpdateHighScore;
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
            _updateManager.GlobalFixedUpdateEvent += LocalFixedUpdateScore;
        }
        else
        {
            _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateScore;
            _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdateHighScore;
            _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
            SaveHighScore();
        }
    }

    private void Pause(bool isResumed)
    {
        scorePanel.SetActive(isResumed);
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

    public void SetScrollSpeed(float scrollSpeed)
    {
        _scorePerSecond = scrollSpeed;
    }
}
