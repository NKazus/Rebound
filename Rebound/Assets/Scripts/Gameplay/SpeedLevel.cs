using UnityEngine;
using Zenject;

public class SpeedLevel : MonoBehaviour, IGlobalScroll
{
    [SerializeField] private GameObject speedLevelPanel;
    [SerializeField] private SpeedValueColor[] levels;
    [SerializeField] private Color color;

    private float _minGlobalSpeed;
    private float _maxGlobalSpeed;
    private float[] _speedBoundaryValues;
    private int _levelsNumber;
    private int _currentLevel;

    [Inject] private readonly GlobalEventManager _eventManager;
    [Inject] private readonly InitConfig _initConfig;

    #region MONO
    private void Awake()
    {
        _minGlobalSpeed = _initConfig.InitialGlobalSpeed;
        _maxGlobalSpeed = _initConfig.MaxScrollSpeed * Mathf.Cos(_initConfig.MinDeflectionAngle * Mathf.Deg2Rad);

        _levelsNumber = levels.Length;
        _speedBoundaryValues = new float[_levelsNumber - 1];
        float speedDelta = (_maxGlobalSpeed - _minGlobalSpeed) / _levelsNumber;
        for (int i = 0; i < _levelsNumber - 1; i++)
        {
            _speedBoundaryValues[i] = _minGlobalSpeed + speedDelta * (i + 1);
        }
    }

    private void OnEnable()
    {
        _eventManager.ResetScrollingSpeedEvent += SetScrollSpeed;
        _eventManager.GameStateEvent += ChangeState;
    }

    private void OnDisable()
    {
        _eventManager.ResetScrollingSpeedEvent -= SetScrollSpeed;
        _eventManager.GameStateEvent -= ChangeState; 
    }
    #endregion

    private void ChangeState(bool isActive)
    {
        speedLevelPanel.SetActive(isActive);
        if (isActive)
        {
            for(int i = 0; i < _levelsNumber; i++)
            {
                levels[i].SetInitialColor(color);
            }
            levels[_currentLevel].ActivateGlowEffect(true);
            _eventManager.PauseEvent += Pause;
        }
        else
        {
            _eventManager.PauseEvent -= Pause;
        }
    }

    private void Pause(bool isResumed)
    {
        speedLevelPanel.SetActive(isResumed);
    }

    public void SetScrollSpeed(float scrollSpeed)
    {
        while((_currentLevel < _levelsNumber - 1) && (scrollSpeed > _speedBoundaryValues[_currentLevel]))
        {
            _currentLevel++;
            levels[_currentLevel].ActivateGlowEffect(true);
        }
    }
}
