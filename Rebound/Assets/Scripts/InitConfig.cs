using UnityEngine;

[CreateAssetMenu(fileName = "InitConfig")]
public class InitConfig : ScriptableObject
{
    [SerializeField] private float initialGlobalSpeed;
    public float InitialGlobalSpeed => initialGlobalSpeed;

    [SerializeField] private float maxScrollSpeed;
    public float MaxScrollSpeed => maxScrollSpeed;

    [SerializeField] private float maxInitialAngle;
    public float MaxInitialAngle => maxInitialAngle;

    [SerializeField] private float minDeflectionAngle;
    public float MinDeflectionAngle => minDeflectionAngle;
}
