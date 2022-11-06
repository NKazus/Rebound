using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startMessage;
    [SerializeField] private TextMeshProUGUI newScoreMessage;
    private Tween _textScaleTween;

    private void Awake()
    {
        startMessage.text = "Choose direction";
        _textScaleTween = DOTween.Sequence()
            .Append(startMessage.DOScale(1.5f, 1f))
            .Append(startMessage.DOScale(1f, 1f))
            .SetLoops(-1);
    }

    private void OnEnable()
    {
        GlobalEventManager.GameStateEvent += SetMessage;
    }

    private void SetMessage(bool isActive)
    {
        if (isActive)
        {
            _textScaleTween.Pause();
            startMessage.DOFade(0f, 0.3f).Play().OnComplete(() => startMessage.enabled = false);
        }
        else
        {
            startMessage.enabled = true;
            startMessage.text = "Tap to replay";
            startMessage.DOFade(1f, 1f).Play();
            _textScaleTween.Play();
        }
    }

    private void OnDisable()
    {
        GlobalEventManager.GameStateEvent -= SetMessage;
        _textScaleTween.Kill();
    }

    public void ShowNewScoreMessage()
    {
        newScoreMessage.enabled = true;
        newScoreMessage.DOText("New high score!", 1f).SetLink(gameObject).Play();
    }
}
