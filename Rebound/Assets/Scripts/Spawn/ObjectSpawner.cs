using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour, GlobalSpeedController.IGlobalScroll
{
    [SerializeField] protected GameObject[] spawnObjectsPrefabs;
    [Header("Position Offset:")]
    [Space(5)]
    [SerializeField] protected float minOffset = -1f;
    [SerializeField] protected float maxOffset = 1f;
    [Header("Initial Scale:")]
    [Space(5)]
    [SerializeField] protected float minScale = 0.7f;
    [SerializeField] protected float maxScale = 1.5f;

    protected Vector3 spawnerPosition;
    protected System.Random randomizer;

    private float _offset;
    private float _speedModifyer = -2f;
    private float _initialSpeedModifyer;
    private float _currentSpawnTime = 5f;   
    private int _spawnObjectsCount;


    protected void Awake()
    {
        var configInfo = Resources.Load<InitConfig>("InitConfig");
        _initialSpeedModifyer = configInfo.InitialGlobalSpeed;

        randomizer = new System.Random();
        _spawnObjectsCount = spawnObjectsPrefabs.Length;
    }

    protected virtual void OnEnable()
    {
        spawnerPosition = transform.position;
    }

    public void StartSpawning(CancellationToken spawnToken)
    {
        Task.Factory.StartNew(() =>
            SpawnObject(spawnToken),
            spawnToken,
            TaskCreationOptions.None,
            TaskScheduler.FromCurrentSynchronizationContext());
    }

    protected async void SpawnObject(CancellationToken spawnToken)
    {
        GameObject spawnObject;
        await Task.Delay(TimeSpan.FromSeconds(_currentSpawnTime));
        while (!spawnToken.IsCancellationRequested)
        {
            spawnObject = PoolManager.GetGameObjectFromPool(spawnObjectsPrefabs[randomizer.Next(_spawnObjectsCount)]);
            SetSpawnObject(spawnObject);
            spawnObject.transform.position = CalculateSpawnPosition();
            await Task.Delay(TimeSpan.FromSeconds(_currentSpawnTime * _initialSpeedModifyer / _speedModifyer));
        }
    }

    protected virtual void SetSpawnObject(GameObject spawnObject)
    {
        spawnObject.transform.localScale = new Vector3(RandomExtentions.NextFloat(randomizer, minScale, maxScale), 1f, 1f);
        spawnObject.GetComponent<Refraction>().SetObjectParameters(RandomExtentions.NextFloat(randomizer, 0f, 1f));
    }

    protected virtual Vector3 CalculateSpawnPosition()
    {
        _offset = RandomExtentions.NextFloat(randomizer, minOffset, maxOffset);
        return new Vector3(spawnerPosition.x + _offset, spawnerPosition.y, spawnerPosition.z);     
    }

    public void SetSpawningTime(float spawnDeltaTime)
    {
        _currentSpawnTime = spawnDeltaTime;
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        _speedModifyer = scrollSpeed;
    }
}
