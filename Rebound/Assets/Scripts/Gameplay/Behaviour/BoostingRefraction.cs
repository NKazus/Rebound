using UnityEngine;
using static Refraction;

public class BoostingRefraction : MonoBehaviour, IRefraction
{
    public float Refract(float value)
    {
        return value += Mathf.Log(value, 2);
    }
}
