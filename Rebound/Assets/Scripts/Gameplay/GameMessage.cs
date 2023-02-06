using DG.Tweening;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Zenject;

public class GameMessage : MonoBehaviour
{
    private const string INITIAL_MESSAGE = "Choose direction";
    private const string REPLAY_MESSAGE = "Tap to replay";
    private const string PROCEED_MESSAGE = "Tap to continue";
    private const string HIGH_SCORE_MESSAGE = "New high score!";

    [SerializeField] private TextMeshProUGUI startMessage;
    [SerializeField] private TextMeshProUGUI newScoreMessage;
    [SerializeField] private TextMeshProUGUI replayMessage;
    private Tween _textScaleTween;

    [Inject] private readonly GlobalEventManager _eventManager;

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
            DOTween.Sequence().SetId(this)
                .Append(startMessage.DOFade(0f, 0.3f))
                .Join(newScoreMessage.DOFade(0f, 0.3f))
                .Join(replayMessage.DOFade(0f, 0.3f))
                .OnComplete(() => startMessage.enabled = newScoreMessage.enabled = false);         
        }
        else
        {
            startMessage.enabled = true;
            startMessage.text = PROCEED_MESSAGE;
            startMessage.DOFade(1f, 1f).SetId(this).Play();
            replayMessage.text = REPLAY_MESSAGE;
            replayMessage.DOFade(1f, 1f).SetId(this).Play();
            _textScaleTween.Play();

        }
    }

    public void ShowNewScoreMessage()
    {
        newScoreMessage.enabled = true;
        newScoreMessage.DOFade(1f, 0.3f).SetId(this).Play();
        newScoreMessage.DOText(HIGH_SCORE_MESSAGE, 1f).SetId(this).Play();
    }
}
