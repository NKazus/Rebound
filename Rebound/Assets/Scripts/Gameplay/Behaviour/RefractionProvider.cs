using System;

public enum RefractionType
{
    Simple,
    Boosting,
    Harmonic,
    Absorbing
}
public class RefractionProvider
{    
    public IRefraction GetRefraction(RefractionType type)
    {
        return type switch
        {
            RefractionType.Boosting => new BoostingRefraction(),
            RefractionType.Harmonic => new HarmonicRefraction(),
            RefractionType.Absorbing => new AbsorbingRefraction(),
            RefractionType.Simple => new SimpleRefraction(),
            _ => throw new NotSupportedException()
        };
    }
}
