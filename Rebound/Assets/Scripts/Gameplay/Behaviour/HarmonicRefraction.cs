using UnityEngine;
using static Refraction;

public class HarmonicRefraction : MonoBehaviour, IRefraction
{
    public float Refract(float value)
    {
        return value += Mathf.Sin(value);
    }
}
