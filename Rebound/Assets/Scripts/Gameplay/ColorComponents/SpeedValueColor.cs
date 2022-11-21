using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedValueColor : ObjectColor
{
    protected override void Awake()
    {
        Image image = GetComponent<Image>();
        objectMaterial = Instantiate(image.material);
        objectMaterial.SetTexture("_MainTex", image.mainTexture);
        objectMaterial.SetTexture("_AlphaTex", alphaTexture);
        objectMaterial.SetFloat("_GlowValue", initialGlowValue);
        image.material = objectMaterial;
    }

    public override void SetColor(float value)
    {
        throw new NotImplementedException();
    }

    public void SetInitialColor(Color initialColor)
    {
        currentColor = initialColor;
        objectMaterial.SetColor("_Color", currentColor * intensity);
    }
}
