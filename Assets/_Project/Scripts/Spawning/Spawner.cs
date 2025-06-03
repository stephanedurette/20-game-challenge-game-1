using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private Dictionary<GameObject, ObjectPool<GameObject>> objectPools;

    private List<GameObject> currentlySpawnedObjects;

    private void Awake()
    {
        objectPools = new();
        currentlySpawnedObjects = new();
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
            currentlySpawnedObjects.Add(p);
        }

        void OnReturnedToPool(GameObject p)
        {
            p.gameObject.SetActive(false);
            currentlySpawnedObjects.Remove(p);
        }

        void OnDestroyPoolObject(GameObject p)
        {
            Destroy(p.gameObject);
        }

        return pool;
    }

    public void DespawnAllObjects()
    {
        foreach (GameObject g in currentlySpawnedObjects) { 
            g.gameObject.SetActive(false);
        }
    }

    private IEnumerator ReturnToPool(GameObject instance, ObjectPool<GameObject> pool)
    {
        yield return new WaitUntil(() => !instance.activeInHierarchy);
        pool.Release(instance);
    }

    public void Spawn(SpawnSettings spawnSettings)
    {
        if (!objectPools.ContainsKey(spawnSettings.SpawnPrefab))
        {
            objectPools[spawnSettings.SpawnPrefab] = InitializePool(spawnSettings.SpawnPrefab);
        }

        GameObject toSpawn = objectPools[spawnSettings.SpawnPrefab].Get();
        StartCoroutine(ReturnToPool(toSpawn, objectPools[spawnSettings.SpawnPrefab]));
        toSpawn.transform.position = spawnSettings.SpawnPosition;

        if (spawnSettings.SpawnAction != null)
        {
            spawnSettings.SpawnAction(toSpawn);
        }
    }
}
