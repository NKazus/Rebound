using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    [SerializeField] private Color primaryColor = Color.red;
    [SerializeField] private Color secondaryColor = Color.yellow;
    [SerializeField] private Texture2D alphaTexture;
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float initialGlowValue = 0f;

    private Material _objectMaterial;
    private Color _currentColor;

    private void Awake()
    {
        _objectMaterial = GetComponent<SpriteRenderer>().material;
        _objectMaterial.SetTexture("_AlphaTex", alphaTexture);
        _objectMaterial.SetColor("_Color", primaryColor * intensity);
        _objectMaterial.SetFloat("_GlowValue", initialGlowValue);
    }

    public Color SetColor(float interpolation)//refraction/trigger init
    {
        _currentColor = Color.Lerp(primaryColor, secondaryColor, interpolation);
        _objectMaterial.SetColor("_Color", _currentColor * intensity);
        return _currentColor;
    }

    public void SetColor(Color colorValue)//reflection
    {
        //_currentColor = colorValue;
        if(_objectMaterial != null)//подумать, как переделать без execution order
            _objectMaterial.SetColor("_Color", colorValue * intensity);//постепенно
    }

    public void ActivateGlowEffect(bool isActive)//refraction
    {
        if (isActive)
        {
            _objectMaterial.SetFloat("_GlowValue", 1f);//постепенно
        }
        else
        {
            _objectMaterial.SetFloat("_GlowValue", 0f);
        }
    }
}
