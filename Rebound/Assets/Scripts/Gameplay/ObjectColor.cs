using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : MonoBehaviour
{
    [SerializeField] private Color primaryColor = Color.red;
    [SerializeField] private Color secondaryColor = Color.yellow;

    private SpriteRenderer _objectRenderer;

    private void Awake()
    {
        _objectRenderer = GetComponent<SpriteRenderer>();
    }

    public Color SetColor(float interpolation)
    {
        _objectRenderer.color = Color.Lerp(primaryColor, secondaryColor, interpolation);
        return _objectRenderer.color;
    }
}
