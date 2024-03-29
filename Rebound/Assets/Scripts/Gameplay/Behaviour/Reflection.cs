using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ObjectColor))]
public class Reflection : ObjectBehaviour
{
    private Transform _transform;
    private Tween _punchScaleTween;
    private Transform _currentObject;
    private Rigidbody2D _playerRigidBody;

    [Inject] private readonly ReflectionController _controller;

    #region MONO
    protected override void Awake()
    {
        _transform = transform;
        _punchScaleTween = _transform.DOPunchScale(new Vector3(0.2f, 0, 0), 0.1f, 5).SetLink(gameObject);
        _soundType = SoundType.Reflection;
        base.Awake();
    }

    private void OnEnable()
    {
        _controller.ReflectionUpdateEvent += UpdateReflection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _currentObject = collision.gameObject.transform;
        float collisionSpeedSign;
        if (_currentObject.CompareTag("Player"))
        {
            if(_playerRigidBody == null)
            {
                _playerRigidBody = _currentObject.GetComponent<Rigidbody2D>();
            }
            collisionSpeedSign = Mathf.Sign(_playerRigidBody.velocity.x);
            if (collisionSpeedSign * (_transform.position.x - _currentObject.position.x) < 0)
            {
                return;
            }
            _eventManager.CalculateSpeed(_controller.ReflectionState.Reflect, true);            
            _objectColor.ActivateGlowEffect(BaseReflection.IsActiveState);
            _punchScaleTween.Play();
            _soundEffect.PlaySound();
        }
    }
    private void OnDisable()
    {
        _controller.ReflectionUpdateEvent -= UpdateReflection;
    }
    #endregion

    private void UpdateReflection(float intensity)
    {
        _objectColor.SetColor(intensity);
    }
}
