using System.Collections.Generic;
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
        //SpawnCar();
    }

    private void SpawnCar(float speed, Vector3 position)
    {
        SpawnSettings settings = new(spawnPrefab, position, (g) =>
        {
            g.GetComponent<Vehicle>().Initialize(speed);
        });
        spawnEvent?.Invoke(settings);
    }
}
