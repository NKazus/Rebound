using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolManager
{
    [Inject] private DiContainer diContainer;
    private Dictionary<string, LinkedList<GameObject>> _poolDictionary = new Dictionary<string, LinkedList<GameObject>>();

    public GameObject GetGameObjectFromPool(GameObject prefab)
    {
        if (!_poolDictionary.ContainsKey(prefab.name))
        {
            _poolDictionary[prefab.name] = new LinkedList<GameObject>();
        }
        GameObject result;
        if (_poolDictionary[prefab.name].Count > 0)
        {
            result = _poolDictionary[prefab.name].First.Value;
            _poolDictionary[prefab.name].RemoveFirst();
            result.SetActive(true);
            return result;
        }

        result = diContainer.InstantiatePrefab(prefab);
        //result = Instantiate(prefab);
        result.name = prefab.name;
        return result;
    }

    public void PutGameObjectToPool(GameObject target)
    {
        _poolDictionary[target.name].AddFirst(target);
        target.SetActive(false);
    }
}
