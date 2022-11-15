using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class BackgroundScroller : MonoBehaviour, IGlobalScroll
{
    private BoxCollider2D _backgroundCollider;
    private Rigidbody2D _backgroundRigidbody;
    private float _backgroundHeight;
    private float _backgroundScrollSpeed;
    private Vector2 _resetPosition;
    private Transform _localTransform;

    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly GlobalEventManager _eventManager;

    #region MONO
    private void Awake()
    {
        _backgroundCollider = GetComponent<BoxCollider2D>();
        _backgroundRigidbody = GetComponent<Rigidbody2D>();

        _localTransform = transform;
        _backgroundHeight = _backgroundCollider.size.y;
        _backgroundCollider.enabled = false;
        _resetPosition = new Vector2(0, _backgroundHeight * _localTransform.localScale.y * 2f);
    }

    private void OnEnable()
    {
        _updateManager.GlobalUpdateEvent += LocalUpdate;
        _eventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
    }
    private void OnDisable()
    {
        _updateManager.GlobalUpdateEvent -= LocalUpdate;
        _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
    }
    #endregion

    private void LocalUpdate()
    {
        if(_localTransform.position.y < -_backgroundHeight * _localTransform.localScale.y)
        {
            _localTransform.position = (Vector2)_localTransform.position + _resetPosition;
        }
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _backgroundScrollSpeed = scrollSpeed;
        _backgroundRigidbody.velocity = new Vector2(0, -_backgroundScrollSpeed);
    }

}
