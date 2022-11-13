using UnityEngine;

public class BoostingRefraction : IRefraction
{
    public float Refract(float value)
    {
        return value += Mathf.Log(value, 2);
    }
}
