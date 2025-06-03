using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CarSpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Transform> carSpawnTransforms;
    [SerializeField] private List<Transform> obstacleSpawnTransforms;
    [SerializeField] private GameObject carSpawnPrefab;
    [SerializeField] private GameObject obstacleSpawnPrefab;
    [SerializeField] private UnityEvent<SpawnSettings> spawnEvent;

    [Header("Spawn Settings")]
    [SerializeField] private float delayBetweenWaves;
    [SerializeField][Range(0, 1)] private float delayBetweenWavesVariance;

    [Header("Car Spawn Settings")]
    [SerializeField] private int minCarsPerWave;
    [SerializeField] private int maxCarsPerWave;
    [SerializeField] private float minCarSpeed;
    [SerializeField] private float maxCarSpeed;

    [Header("Obstacle Spawn Settings")]
    [SerializeField] private int minObstaclesPerWave;
    [SerializeField] private int maxObstaclesPerWave;

    private SpawnSettings carSpawnSettings;
    private SpawnSettings obstacleSpawnSettings;

    private void Awake()
    {
        carSpawnSettings = new();
        carSpawnSettings.SpawnPrefab = carSpawnPrefab;

        obstacleSpawnSettings = new(obstacleSpawnPrefab, Vector3.zero, (g) =>
        {
            g.GetComponent<Obstacle>().Initialize();
        });
    }

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
            SpawnWave(carSpawnTransforms, minCarsPerWave, maxCarsPerWave, SpawnCar);
            SpawnWave(obstacleSpawnTransforms, minObstaclesPerWave, maxObstaclesPerWave, SpawnObstacle);
        }
    }

    private void SpawnWave(List<Transform> transforms, int minAmount, int maxAmount, Action<Vector3> spawnAction)
    {
        int numSpawns = UnityEngine.Random.Range(minAmount, maxAmount + 1);
        List<Transform> shuffledTransforms = transforms.OrderBy(_ => Guid.NewGuid()).ToList();

        for (int i = 0; i < numSpawns; i++)
        {
            spawnAction(shuffledTransforms[i].position);
        }

    }

    private void SpawnObstacle(Vector3 position)
    {
        obstacleSpawnSettings.SpawnPosition = position;
        spawnEvent?.Invoke(obstacleSpawnSettings);
    }

    private void SpawnCar(Vector3 position)
    {
        carSpawnSettings.SpawnPosition = position;
        carSpawnSettings.SpawnAction = (g) => { g.GetComponent<Vehicle>().Initialize(UnityEngine.Random.Range(minCarSpeed, maxCarSpeed), position); };
        spawnEvent?.Invoke(carSpawnSettings);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
