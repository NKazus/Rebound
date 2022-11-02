using UnityEngine;

public class ObjectScroller : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    private Rigidbody2D _rigidbody;
    private float _scrollSpeed;
    private float _despawnPosition;
    private Transform _localTransform;

    private void Awake()
    {
        _despawnPosition = GameObject.FindWithTag("PoolLimit").transform.position.y;
        _rigidbody = GetComponent<Rigidbody2D>();
        _localTransform = gameObject.transform;
    }

    private void OnEnable()
    {
        _scrollSpeed = GlobalSpeedController.ScrollSpeed;
        _rigidbody.velocity = new Vector2(0f, -_scrollSpeed);
        GlobalUpdateManager.GlobalUpdateEvent += LocalUpdate;
        GlobalEventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
    }

    private void LocalUpdate()
    {
        if(_localTransform.position.y < _despawnPosition)
        {
            PoolManager.PutGameObjectToPool(gameObject);
        }
    }

    private void OnDisable()
    {
        GlobalUpdateManager.GlobalUpdateEvent -= LocalUpdate;
        GlobalEventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _scrollSpeed = scrollSpeed;
        _rigidbody.velocity = new Vector2(0f, -_scrollSpeed);
    }

}
