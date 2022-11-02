using UnityEngine;
using static Refraction;

public class AbsorbingRefraction : MonoBehaviour, IRefraction
{
    public float Refract(float value)
    {
        return -1f;
    }
}
