using UnityEngine;
using UnityEngine.Events;

public class CarSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private UnityEvent<SpawnSettings> spawnEvent;

    void Start()
    {
        SpawnCar();
    }

    private void SpawnCar()
    {
        SpawnSettings settings = new(spawnPrefab, spawnTransform.position);
        settings.SpawnAction = (g) => {
            g.GetComponent<Vehicle>().Initialize(4);
        };

        spawnEvent?.Invoke(settings);
    }
}
