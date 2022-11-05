using DG.Tweening;
using UnityEngine;

public abstract class ObjectColor : MonoBehaviour
{
    [SerializeField] protected Texture2D alphaTexture;
    [SerializeField] protected float intensity = 5f;
    [SerializeField] protected float initialGlowValue = 0f;

    protected Material objectMaterial;
    protected Color currentColor;

    protected virtual void Awake()
    {
        objectMaterial = GetComponent<SpriteRenderer>().material;
        objectMaterial.SetTexture("_AlphaTex", alphaTexture);
        objectMaterial.SetFloat("_GlowValue", initialGlowValue);
    }

    public abstract void SetColor(float interpolation);

    public virtual void ActivateGlowEffect(bool isActive)
    {
        if (isActive)
        {
            objectMaterial.DOFloat(1f, "_GlowValue", 0.1f);
        }
        else
        {
            objectMaterial.DOFloat(0f, "_GlowValue", 0.1f);
        }
    }

    public Color GetColor()
    {
        return currentColor;
    }
}
