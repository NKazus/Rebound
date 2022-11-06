using DG.Tweening;
using UnityEngine;

public class PlayerColor : ObjectColor
{
    [SerializeField] private Color[] colors;
    private float _minDeltaAngle;
    private float _intervalLength;
    private float _currentAngle;
    private int _leftIntervalIndex;
    private float _interpolation;
   
    protected override void Awake()
    {
        base.Awake();
        var configInfo = Resources.Load<InitConfig>("InitConfig");
        _minDeltaAngle = configInfo.MinDeflectionAngle;
        _intervalLength = (90f - _minDeltaAngle) / (colors.Length - 1);
        objectMaterial.SetColor("_Color", Color.white * intensity / 2);
    }

    public override void SetColor(float angleSin)
    {
        _currentAngle = Mathf.Asin(angleSin) * Mathf.Rad2Deg;
        _leftIntervalIndex = (int)((_currentAngle - _minDeltaAngle) / _intervalLength);
        _interpolation = (_currentAngle - _minDeltaAngle) % _intervalLength;
        currentColor = Color.Lerp(colors[_leftIntervalIndex], colors[_leftIntervalIndex + 1], _interpolation);
        currentColor.a = 1f;
        objectMaterial.DOColor(currentColor * intensity, "_Color", 0.2f).Play();
    }

}
