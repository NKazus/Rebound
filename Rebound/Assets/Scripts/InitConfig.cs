using UnityEngine;

[CreateAssetMenu(fileName = "InitConfig")]
public class InitConfig : ScriptableObject
{
    [SerializeField] private float _initialGlobalSpeed;
    public float InitialGlobalSpeed => _initialGlobalSpeed;

    [SerializeField] private float _maxScrollSpeed;
    public float MaxScrollSpeed => _maxScrollSpeed;

    [SerializeField] private float _maxInitialAngle;
    public float MaxInitialAngle => _maxInitialAngle;

    [SerializeField] private float _minDeflectionAngle;
    public float MinDeflectionAngle => _minDeflectionAngle;
}
