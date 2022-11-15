using UnityEngine;

public class InputValidator : MonoBehaviour
{
    [SerializeField] private RectTransform _reloadArea;
    [SerializeField] private RectTransform _restartButtonArea;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public bool ValidateGameplayInput(Vector2 inputPosition)
    {
        bool isValid = !RectTransformUtility.RectangleContainsScreenPoint(_restartButtonArea, inputPosition, _mainCamera);
        return isValid;
    }

    public bool ValidateReplayInput(Vector2 inputPosition)
    {
        bool isValid = RectTransformUtility.RectangleContainsScreenPoint(_reloadArea, inputPosition, _mainCamera);
        return isValid;
    }
}
