using UnityEngine;

[CreateAssetMenu(fileName = "InitConfig")]
public class InitConfig : ScriptableObject
{
    [SerializeField] private float initialGlobalSpeed;
    public float InitialGlobalSpeed => initialGlobalSpeed;

    [SerializeField, Tooltip("Vertical speed upper limit")] 
    private float maxScrollSpeed;
    public float MaxScrollSpeed => maxScrollSpeed;

    [SerializeField, Tooltip("Maximum deviation from the vertical axis (< 90)")] 
    private float maxInitialAngle;
    public float MaxInitialAngle => maxInitialAngle;

    [SerializeField, Tooltip("Minimum deviation from the vertical axis ( > 0)")] 
    private float minDeflectionAngle;
    public float MinDeflectionAngle => minDeflectionAngle;

    [SerializeField, Tooltip("Speed reduction after watching an add (percentage)")] 
    private float speedReductionCoefficient;
    public float SpeedReductionCoefficient => speedReductionCoefficient;
}
