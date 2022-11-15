using System.Threading;
using UnityEngine;
using Zenject;

public class SpawnManager : MonoBehaviour, IGlobalScroll
{
    [SerializeField] private ObjectSpawner[] objectSpawners;
    [SerializeField] private float spawnUpdateTimeCoefficient = 30f;
    [SerializeField] private float maxSpawnTimeInterval = 5f; 

    private int _spawnerCount;
    private float[] _spawnTimeIntervals;
    private float _currentCheckValue = 0f;
    private float _maxCheckValue;
    private float _scrollSpeed;
    private System.Random _random;
    private CancellationTokenSource _spawnCancelTokenSource;
    [Inject] private readonly GlobalUpdateManager _updateManager;
    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly InitConfig _initConfig;

    #region MONO
    private void Awake()
    {
        _maxCheckValue = _initConfig.InitialGlobalSpeed * spawnUpdateTimeCoefficient;

        _spawnCancelTokenSource = new CancellationTokenSource();
        _random = new System.Random();

        _spawnerCount = objectSpawners.Length;
        CalculateTimeIntervals();
    }

    private void OnEnable()
    {
        _eventManager.GameStateEvent += ChangeSpawnState;
        _eventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
    }
    private void OnDisable()
    {
        _eventManager.GameStateEvent -= ChangeSpawnState;
        _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;

        CancelTokenSource();
        _spawnCancelTokenSource.Dispose();
    }
    #endregion

    private void ChangeSpawnState(bool isActive)
    {
        if (isActive)
        {
            for (int i = 0; i < _spawnerCount; i++)
            {
                objectSpawners[i].SetSpawningTime(_spawnTimeIntervals[i]);
                objectSpawners[i].StartSpawning(_spawnCancelTokenSource.Token);
            }
            _updateManager.GlobalFixedUpdateEvent += LocalFixedUpdate;          
        }
        else
        {
            _updateManager.GlobalFixedUpdateEvent -= LocalFixedUpdate;
            _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;

            CancelTokenSource();
        }
    }

    private void LocalFixedUpdate()
    {
        _currentCheckValue += Time.fixedDeltaTime * _scrollSpeed;
        if (_currentCheckValue > _maxCheckValue)
        {
            CalculateTimeIntervals();
            for (int i = 0; i < _spawnerCount; i++)
            {
                objectSpawners[i].SetSpawningTime(_spawnTimeIntervals[i]);
            }
            _currentCheckValue = 0f;
        }
    }

    private void CalculateTimeIntervals()
    {
        if (_spawnTimeIntervals == null)
        {
            float currentMax = 0;
            _spawnTimeIntervals = new float[_spawnerCount];
            for(int i = 0; i < _spawnerCount - 1; i++)
            {
                _spawnTimeIntervals[i] = RandomExtentions.NextFloat(_random, maxSpawnTimeInterval / 2, maxSpawnTimeInterval);
                if (_spawnTimeIntervals[i] > currentMax)
                {
                    currentMax = _spawnTimeIntervals[i];
                }
            }
            _spawnTimeIntervals[_spawnerCount - 1] = currentMax * 1.5f;
        }
        else
        {
            ShuffleTimeIntervals();
        }
    }

    private void ShuffleTimeIntervals()
    {
        for (int i = 0; i < _spawnerCount; i++)
        {
            _spawnTimeIntervals[i] -= _spawnTimeIntervals[i] * 0.05f;
        }
        int j;
        float temp;
        for (int i = _spawnerCount - 2; i >= 1; i--)
        {
            j = _random.Next(i + 1);
            temp = _spawnTimeIntervals[j];
            _spawnTimeIntervals[j] = _spawnTimeIntervals[i];
            _spawnTimeIntervals[i] = temp;
        }
    }

    private void CancelTokenSource()
    {
        if(!_spawnCancelTokenSource.IsCancellationRequested)
        {
            _spawnCancelTokenSource.Cancel();
        }
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _scrollSpeed = scrollSpeed;
        for(int i = 0; i < _spawnerCount; i++)
        {
            objectSpawners[i].SetScrollSpeed(_scrollSpeed);
        }
    }
}
