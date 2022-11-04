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

    public void ActivateGlowEffect(bool isActive)//refraction
    {
        if (isActive)
        {
            objectMaterial.SetFloat("_GlowValue", 1f);//постепенно
        }
        else
        {
            objectMaterial.SetFloat("_GlowValue", 0f);
        }
    }

    public Color GetColor()
    {
        return currentColor;
    }
}
