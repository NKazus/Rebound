public class AbsorbingRefraction : IRefraction
{
    public float Refract(float value)
    {
        return -1f;
    }

    public void Setup(float coefficient)
    {
        return;
    }
}
