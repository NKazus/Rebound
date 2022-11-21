using UnityEngine;
using Zenject;

public class ParticleTrail : MonoBehaviour, IGlobalScroll
{
    [SerializeField] private ParticleSystem particleTrail;

    private ParticleSystem.VelocityOverLifetimeModule _velocity;
    private ParticleSystem.MainModule _mainModule;

    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerColor _playerColor;
    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        _velocity = particleTrail.velocityOverLifetime;
        _velocity.enabled = false;

        _mainModule = particleTrail.main;
    }

    private void OnEnable()
    {
        _eventManager.GameStateEvent += ChangeState;
        _eventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
    }

    private void OnDisable()
    {
        _eventManager.GameStateEvent -= ChangeState;
        _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
    }
    #endregion

    private void ChangeState(bool isActive)
    {
        if (isActive)
        {
            _velocity.enabled = true;
        }
        else
        {
            _velocity.enabled = false;
        }
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _mainModule.startColor = _playerColor.GetColor();
        _velocity.x = (-1f) * _playerMovement.GetPlayerSpeed();
        _velocity.y = (-1f) * scrollSpeed;
    }
}
