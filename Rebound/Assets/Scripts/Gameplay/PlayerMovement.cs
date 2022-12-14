using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private Transform _transform;
    private float _movementSpeedAbs;
    private float _movementSpeedSign;
    private Vector2 _initialPosition;

    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _transform = transform;
        _initialPosition = _transform.position;
    }

    private void OnEnable()
    {
        _eventManager.GameStateEvent += Eliminate;
    }
    private void OnDisable()
    {
        _eventManager.GameStateEvent -= Eliminate;
        DOTween.Kill(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _transform.DOScale(1.1f, 0.05f).SetId(this).Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _transform.DOScale(1f, 0.05f).SetId(this).Play();
    }
    #endregion

    private void SetPlayerSpeed()
    {
        _playerRigidBody.velocity = new Vector2(_movementSpeedSign * _movementSpeedAbs, 0f);
    }

    public void ReversePlayerSpeedSign()
    {
        _movementSpeedSign = -_movementSpeedSign;
        SetPlayerSpeed();
    }

    public void SetPlayerSpeedAbs(float newSpeedAbs)
    {
        _movementSpeedAbs = newSpeedAbs;
        SetPlayerSpeed();
    }

    public void SetInitialSpeedSign(float initialSpeedSign)
    {
        _movementSpeedSign = initialSpeedSign;
    }

    public float GetPlayerSpeed()
    {
        return _movementSpeedAbs * _movementSpeedSign;
    }

    public void Eliminate(bool isActive)
    {
        if (!isActive)
        {
            _playerRigidBody.velocity = Vector2.zero;
            DOTween.Sequence().SetId(this)
                .Append(_transform.DOShakeScale(0.3f, 0.5f, 5, 50))
                .Append(_transform.DOScale(0f, 0.1f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void ResetPlayerPosition()
    {
        _transform.localScale = Vector2.zero;
        _transform.position = _initialPosition;
        _transform.DOScale(1f, 0.1f);
        ReversePlayerSpeedSign();
    }
}
