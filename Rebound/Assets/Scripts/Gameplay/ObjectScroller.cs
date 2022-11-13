using UnityEngine;
using Zenject;

public class ObjectScroller : MonoBehaviour, IGlobalScroll
{
    private Rigidbody2D _rigidbody;
    private float _scrollSpeed;
    private float _despawnPosition;
    private Transform _localTransform;
    [Inject] private GlobalUpdateManager _updateManager;
    [Inject] private GlobalEventManager _eventManager;
    [Inject] private PoolManager _pool;

    #region MONO
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
        if(_localTransform.position.y < _despawnPosition)
        {
            _pool.PutGameObjectToPool(gameObject);
        }
    }


    public void SetScrollSpeed(float scrollSpeed)
    {
        _scrollSpeed = scrollSpeed;
        _rigidbody.velocity = new Vector2(0f, -_scrollSpeed);
    }

}
