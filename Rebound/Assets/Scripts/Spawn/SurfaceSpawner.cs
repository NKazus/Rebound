using UnityEngine;
using Zenject;

public class SurfaceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _surfacePrefab;
    [Inject] private GameplayInput _input;
    [Inject] private PoolManager _pool;

    #region MONO
    private void OnEnable()
    {
        _input.PlayerInputEvent += Spawn;
    }
	private void OnDisable()
    {
        _input.PlayerInputEvent -= Spawn;
    }
    #endregion

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject currentSurface = _pool.GetGameObjectFromPool(_surfacePrefab);       
        currentSurface.transform.position = spawnPosition;
    }
}
