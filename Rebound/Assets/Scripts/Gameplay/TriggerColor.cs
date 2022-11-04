using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColor : ObjectColor
{
    [SerializeField] private Color primaryColor = Color.red;
    [SerializeField] private Color secondaryColor = Color.yellow;

    public override void SetColor(float interpolation)
    {
        currentColor = Color.Lerp(primaryColor, secondaryColor, interpolation);
        objectMaterial.SetColor("_Color", currentColor * intensity);
    }
}
