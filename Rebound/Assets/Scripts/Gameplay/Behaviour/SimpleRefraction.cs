public class SimpleRefraction : IRefraction
{
    public float Refract(float value)
    {
        return value + 0.1f;
    }
}
