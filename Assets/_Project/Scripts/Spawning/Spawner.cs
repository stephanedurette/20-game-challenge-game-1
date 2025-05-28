using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private Dictionary<GameObject, ObjectPool<GameObject>> objectPools;

    private void Awake()
    {
        objectPools = new();
    }

    private ObjectPool<GameObject> InitializePool(GameObject prefab)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            OnCreate,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            true
        );

        GameObject OnCreate()
        {
            GameObject p = Instantiate(prefab, transform);
            p.gameObject.SetActive(false);
            return p;
        }

        void OnTakeFromPool(GameObject p)
        {
            p.gameObject.SetActive(true);
        }

        void OnReturnedToPool(GameObject p)
        {
            p.gameObject.SetActive(false);
        }

        void OnDestroyPoolObject(GameObject p)
        {
            Destroy(p.gameObject);
        }

        return pool;
    }

    public void Spawn(SpawnSettings spawnSettings)
    {
        if (!objectPools.ContainsKey(spawnSettings.SpawnPrefab)) {
            objectPools[spawnSettings.SpawnPrefab] = InitializePool(spawnSettings.SpawnPrefab);
        }

        GameObject toSpawn = objectPools[spawnSettings.SpawnPrefab].Get();
        toSpawn.transform.position = spawnSettings.SpawnPosition;

        if (spawnSettings.SpawnAction != null) {
            spawnSettings.SpawnAction(toSpawn);
        }
    }
}
