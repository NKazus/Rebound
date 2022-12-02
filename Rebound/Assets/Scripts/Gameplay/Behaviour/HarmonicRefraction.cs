using UnityEngine;

public class HarmonicRefraction : IRefraction
{
    private float _coefficient = 1f;

    public float Refract(float value)
    {
        return value += _coefficient * Mathf.Sin(value);
    }

    public void Setup(float coefficient)
    {
        _coefficient = coefficient;
    }
}
