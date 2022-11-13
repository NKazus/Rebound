using UnityEngine;

public class HarmonicRefraction : IRefraction
{
    public float Refract(float value)
    {
        return value += Mathf.Sin(value);
    }
}
