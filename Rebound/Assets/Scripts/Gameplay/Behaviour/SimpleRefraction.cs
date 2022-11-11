using UnityEngine;
using static Refraction;

public class SimpleRefraction : MonoBehaviour, IRefraction
{
    public float Refract(float value)
    {
        return value + 0.1f;
    }
}
