using UnityEngine;

public class BoostingRefraction : IRefraction
{
    private float _coefficient = 1f;

    public float Refract(float value)
    {
        return value += Mathf.Log(value, _coefficient);
    }

    public void Setup(float coefficient)
    {
        _coefficient = coefficient;
    }
}
