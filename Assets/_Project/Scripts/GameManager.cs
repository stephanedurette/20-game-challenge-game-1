using NUnit.Framework;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private RoadChunk[] chunks;
    [SerializeField] private Spawner spawner;
    [SerializeField] private ObstacleSpawnManager obstacleSpawnManager;

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Cleanup();

        spawner.DespawnAllObjects();
        foreach (var chunk in chunks) chunk.ResetChunk();
        player.ResetToStart();
        obstacleSpawnManager.StartSpawn();
    }

    private void Cleanup()
    {
        obstacleSpawnManager.StopSpawn();
    }
}
