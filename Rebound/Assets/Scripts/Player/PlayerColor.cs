
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    private SpriteRenderer _playerSpriteRenderer;
    private float _minDeltaAngle;
    private float _intervalLength;
    private float _currentAngle;
    private int _leftIntervalIndex;
    private float _interpolation;
    private Color _currentColor;
   
    private void Awake()
    {
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        var configInfo = Resources.Load<InitConfig>("InitConfig");
        _minDeltaAngle = configInfo.MinDeflectionAngle;
        _intervalLength = (90f - _minDeltaAngle) / (colors.Length - 1);
    }

    public void ResetPlayerColor(float angleSin)
    {
        _currentAngle = Mathf.Asin(angleSin) * Mathf.Rad2Deg;
        _leftIntervalIndex = (int)((_currentAngle - _minDeltaAngle) / _intervalLength);
        _interpolation = (_currentAngle - _minDeltaAngle) % _intervalLength;
        _currentColor = Color.Lerp(colors[_leftIntervalIndex], colors[_leftIntervalIndex + 1], _interpolation);
        _currentColor.a = 1f;
        _playerSpriteRenderer.color = _currentColor;
    }
}
