using UnityEngine;

public class RefractionColor : ObjectColor// + TriggerColor
{
    [SerializeField] private Color primaryColor = Color.red;
    [SerializeField] private Color secondaryColor = Color.yellow;

    public override void SetColor(float interpolation)
    {
        currentColor = Color.Lerp(primaryColor, secondaryColor, interpolation);
        objectMaterial.SetColor("_Color", currentColor * intensity);
    }
}
