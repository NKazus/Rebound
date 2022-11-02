using UnityEngine;

public class BackgroundScroller : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    private BoxCollider2D _backgroundCollider;
    private Rigidbody2D _backgroundRigidbody;
    private float _backgroundHeight;
    private float _backgroundScrollSpeed;
    private float _verticalScale;
    private Vector2 _resetPosition;
    private Transform _localTransform;


    private void Awake()
    {
        _backgroundCollider = GetComponent<BoxCollider2D>();
        _backgroundRigidbody = GetComponent<Rigidbody2D>();

        _localTransform = transform;
        _backgroundHeight = _backgroundCollider.size.y;
        _backgroundCollider.enabled = false;
        _verticalScale = transform.localScale.y;
        _resetPosition = new Vector2(0, _backgroundHeight * _verticalScale * 2f);
    }

    private void OnEnable()
    {
        GlobalUpdateManager.GlobalUpdateEvent+=LocalUpdate;
        GlobalEventManager.ResetScrollingSpeedEvent+=SetScrollSpeed;
    }

    private void LocalUpdate()
    {
        if(_localTransform.position.y < -_backgroundHeight * _verticalScale)
        {
            _localTransform.position = (Vector2)_localTransform.position + _resetPosition;
        }
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _backgroundScrollSpeed = scrollSpeed;
        _backgroundRigidbody.velocity = new Vector2(0, -_backgroundScrollSpeed);
    }

    private void OnDisable()
    {
        GlobalUpdateManager.GlobalUpdateEvent -= LocalUpdate;
        GlobalEventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
    }
}
