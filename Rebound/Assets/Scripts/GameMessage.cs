using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class GameMessage : MonoBehaviour
{
    private const string INITIAL_MESSAGE = "Choose direction";
    private const string REPLAY_MESSAGE = "Tap to replay";
    private const string HIGH_SCORE_MESSAGE = "New high score!";

    [SerializeField] private TextMeshProUGUI startMessage;
    [SerializeField] private TextMeshProUGUI newScoreMessage;
    private Tween _textScaleTween;
    [Inject] private GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        startMessage.text = INITIAL_MESSAGE;
        _textScaleTween = DOTween.Sequence().SetId(this)
            .Append(startMessage.DOScale(1.5f, 1f))
            .Append(startMessage.DOScale(1f, 1f))
            .SetLoops(-1);
    }

    private void OnEnable()
    {
        _eventManager.GameStateEvent += SetMessage;
    }

    private void OnDisable()
    {
        _eventManager.GameStateEvent -= SetMessage;
        DOTween.Kill(this);
    }
    #endregion

    private void SetMessage(bool isActive)
    {
        if (isActive)
        {
            _textScaleTween.Pause();
            startMessage.DOFade(0f, 0.3f).SetId(this).Play().OnComplete(() => startMessage.enabled = false);
        }
        else
        {
            startMessage.enabled = true;
            startMessage.text = REPLAY_MESSAGE;
            startMessage.DOFade(1f, 1f).SetId(this).Play();
            _textScaleTween.Play();
        }
    }

    public void ShowNewScoreMessage()
    {
        newScoreMessage.enabled = true;
        newScoreMessage.DOText(HIGH_SCORE_MESSAGE, 1f).SetId(this).Play();
    }
}
