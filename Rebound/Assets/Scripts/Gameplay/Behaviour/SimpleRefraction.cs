public class SimpleRefraction : IRefraction
{
    private float _coefficient = 1f;

    public float Refract(float value)
    {
        return value + _coefficient;
    }

    public void Setup(float coefficient)
    {
        _coefficient = coefficient;
    }
}
