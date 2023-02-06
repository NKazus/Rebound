using UnityEngine;

public class InputValidator : MonoBehaviour
{
    [SerializeField] private RectTransform reloadArea;
    [SerializeField] private RectTransform proceedArea;
    [SerializeField] private RectTransform restartButtonArea;
    [SerializeField] private RectTransform pauseButtonArea;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public bool ValidateGameplayInput(Vector2 inputPosition)
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(restartButtonArea, inputPosition, _mainCamera))
        {
            return false;
        }
        if (RectTransformUtility.RectangleContainsScreenPoint(pauseButtonArea, inputPosition, _mainCamera))
        {
            return false;
        }
        return true;
    }

    public bool ValidateReplayInput(Vector2 inputPosition)
    {
        bool isValid = RectTransformUtility.RectangleContainsScreenPoint(reloadArea, inputPosition, _mainCamera);
        return isValid;
    }

    public bool ValidateRewardInput(Vector2 inputPosition)
    {
        bool isValid = RectTransformUtility.RectangleContainsScreenPoint(proceedArea, inputPosition, _mainCamera);
        return isValid;
    }
}
