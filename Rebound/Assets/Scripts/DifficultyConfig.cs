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

    [SerializeField] private float globalSpeedCoefficient;
    public float GlobalSpeedCoefficient => globalSpeedCoefficient;

    [SerializeField] private float initialSpeedCoefficient;
    public float InitialSpeedCoefficient => initialSpeedCoefficient;

    [SerializeField] private float triggerDuration;
    public float TriggerDuration => triggerDuration;

    [SerializeField] private float simpleCoefficient;
    public float SimpleCoefficient => simpleCoefficient;

    [SerializeField] private float harmonicCoefficient;
    public float HarmonicCoefficient => harmonicCoefficient;

    [SerializeField] private float boostingCoefficient;
    public float BoostingCoefficient => boostingCoefficient;
}
