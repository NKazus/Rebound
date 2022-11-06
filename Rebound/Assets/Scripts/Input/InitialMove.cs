using UnityEngine;

public class InitialMove : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Camera _mainCamera;
    private Vector3 _playerPosition;
    private Vector3 _initialTouchPosition;
    private Vector3 _defaultDirection; //краевое значение по умолчанию
    private Vector3 _currentDirection; //текущий выбранный вектор, к которому применяются ограничения
    private Vector3 _initialDirection; //итоговый вектор, с которого берутся параметры
    private float _maxInitialAngle = 75;


    private void Awake()
    {
        _mainCamera = Camera.main;
        _playerPosition = transform.position;

        _lineRenderer = GetComponent<LineRenderer>();

        var configInfo = Resources.Load<InitConfig>("InitConfig");
        _maxInitialAngle = configInfo.MaxInitialAngle;
        _defaultDirection = Quaternion.AngleAxis(-_maxInitialAngle, Vector3.forward) * Vector3.up;
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            CalculateDirection();
        }
        if (Input.GetMouseButtonUp(0))
        {            
            GlobalEventManager.InitializeMovement(_initialDirection);
            gameObject.SetActive(false);
        }     
    }

    private void CalculateDirection()
    {        
        _initialTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _initialTouchPosition.z = 0f;
        _currentDirection = _initialTouchPosition - _playerPosition;
        if (Vector3.Angle(Vector3.up, _currentDirection) <= _maxInitialAngle)
        {
            _initialDirection = _currentDirection;
        }
        else
        {
            _initialDirection = _defaultDirection;
            _initialDirection.x *= Mathf.Sign(_currentDirection.x);
        }
        ShowDirection();
    }

    private void ShowDirection()
    {
        _lineRenderer.SetPosition(0, _playerPosition);
        _lineRenderer.SetPosition(1, _initialDirection.normalized * 2f + _playerPosition);
    }

}
