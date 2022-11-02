using UnityEngine;

public class SurfaceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _surfacePrefab;

    private void OnEnable()
    {
        GameplayInput.PlayerInputEvent += Spawn;
    }

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject currentSurface = PoolManager.GetGameObjectFromPool(_surfacePrefab);       
        currentSurface.transform.position = spawnPosition;
    }

	private void OnDisable()
    {
        GameplayInput.PlayerInputEvent -= Spawn;
    }

}
