using System;

public static class RandomExtentions
{
    public static float NextFloat(Random r, float min, float max)
    {
        return min + (float)r.NextDouble() * (max - min);
    }

    public static float NextFloat(Random r, float max)
    {
        return (float)r.NextDouble() * max;
    }
}
