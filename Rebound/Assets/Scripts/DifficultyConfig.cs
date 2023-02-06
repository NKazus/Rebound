using UnityEngine;

public enum DifficultyLevel
{
    Easy = 1,
    Normal = 2,
    Hard = 3
}

[CreateAssetMenu(fileName = "DifficultyConfig")]
public class DifficultyConfig : ScriptableObject
{
    [SerializeField] private DifficultyLevel difficultyLevel;
    public DifficultyLevel DifficultyLevel => difficultyLevel;

    [SerializeField, Tooltip("Determines the maximum scroll speed multiplyer (< 2)")] 
    private float globalSpeedCoefficient;
    public float GlobalSpeedCoefficient => globalSpeedCoefficient;

    [SerializeField, Tooltip("Determines the initial global speed multiplyer (< 2)")] 
    private float initialSpeedCoefficient;
    public float InitialSpeedCoefficient => initialSpeedCoefficient;

    [SerializeField, Tooltip("Speed reduction while reflection is active (percentage)")] 
    private float reducingCoefficient;
    public float ReducingCoefficient => reducingCoefficient;

    [SerializeField, Tooltip("Reflection activation time multiplyer")] 
    private float triggerDuration;
    public float TriggerDuration => triggerDuration;

    [SerializeField, Tooltip("Acceleration when passing a simple obstacle (float value < 0.5)")] 
    private float simpleCoefficient;
    public float SimpleCoefficient => simpleCoefficient;

    [SerializeField, Tooltip("Speed multiplyer when passing a harmonic obstacle")] 
    private float harmonicCoefficient;
    public float HarmonicCoefficient => harmonicCoefficient;

    [SerializeField, Tooltip("Acceleration when passing a boosting obstacle")] 
    private float boostingCoefficient;
    public float BoostingCoefficient => boostingCoefficient;
}
