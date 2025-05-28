using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CarSpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Transform> spawnTransforms;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private UnityEvent<SpawnSettings> spawnEvent;

    [Header("Spawn Settings")]
    [SerializeField] private float delayBetweenWaves;
    [SerializeField][Range(0, 1)] private float delayBetweenWavesVariance;
    [SerializeField] private int minCarsPerWave;
    [SerializeField] private int maxCarsPerWave;
    [SerializeField] private float minCarSpeed;
    [SerializeField] private float maxCarSpeed;

    void Start()
    {
        StartCoroutine(SpawnCarCoroutine());
    }

    private IEnumerator SpawnCarCoroutine()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(delayBetweenWaves * (1 - delayBetweenWavesVariance), delayBetweenWaves * (1 + delayBetweenWavesVariance));
            yield return new WaitForSeconds(delay);
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        int numCars = UnityEngine.Random.Range(minCarsPerWave, maxCarsPerWave + 1);
        List<Transform> shuffledTransforms = spawnTransforms.OrderBy(_ => Guid.NewGuid()).ToList();

        for (int i = 0; i < numCars; i++)
        {
            float speed = UnityEngine.Random.Range(minCarSpeed, maxCarSpeed);
            SpawnCar(speed, shuffledTransforms[i].position);
        }

    }

    private void SpawnCar(float speed, Vector3 position)
    {
        SpawnSettings settings = new(spawnPrefab, position, (g) =>
        {
            g.GetComponent<Vehicle>().Initialize(speed);
        });
        spawnEvent?.Invoke(settings);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
