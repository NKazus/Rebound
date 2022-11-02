using UnityEngine;
using static Refraction;

public class SimpleRefraction : MonoBehaviour, IRefraction
{
    //можно настраивать коэффициент для конкретного компонента при спавне - доп методы
    public float Refract(float value)
    {
        return value += value * 0.05f;
    }
}
