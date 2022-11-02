using UnityEngine;

public class TriggerSpawner : ObjectSpawner
{
    [Header("Trigger Settings-------------------------")]
    [Space(10)]
    [SerializeField] private Transform[] triggerSpawnPoints;
    [Header("Active Reflection Time:")]
    [Space(5)]
    [SerializeField] private float minActivationTime = 1f;
    [SerializeField] private float maxActivationTime = 1.5f;

    private int _triggerCount;

    protected override void OnEnable()
    {
        _triggerCount = triggerSpawnPoints.Length;
    }

    protected override void SetSpawnObject(GameObject spawnObject)
    {
        float scale = RandomExtentions.NextFloat(randomizer, minScale, maxScale);
        spawnObject.transform.localScale = new Vector3(scale, scale, 1f);
        spawnObject.GetComponent<ReflectionTrigger>().
            SetTriggerParameters(RandomExtentions.NextFloat(randomizer, minActivationTime, maxActivationTime), 
            RandomExtentions.NextFloat(randomizer, 0f, 1f));
    }

    protected override Vector3 CalculateSpawnPosition()
    {
        spawnerPosition = triggerSpawnPoints[randomizer.Next(_triggerCount)].position;
        return base.CalculateSpawnPosition();
    }
}
