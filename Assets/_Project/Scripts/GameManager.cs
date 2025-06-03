using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private RoadChunk[] chunks;
    [SerializeField] private Spawner spawner;
    [SerializeField] private ObstacleSpawnManager obstacleSpawnManager;
    [SerializeField] private CinemachineCamera playerfollowCamera;
    [SerializeField] private ScoreManager scoreManager;

    private Vector3 playerFollowCameraStartPosition;

    private void Awake()
    {
        playerFollowCameraStartPosition = playerfollowCamera.transform.position;
    }

    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Cleanup();

        scoreManager.ResetScore();
        spawner.DespawnAllObjects();
        foreach (var chunk in chunks) chunk.ResetChunk();
        player.ResetToStart();
        TeleportCameraToStart();
        obstacleSpawnManager.StartSpawn();
    }

    private void Cleanup()
    {
        obstacleSpawnManager.StopSpawn();
    }

    private void TeleportCameraToStart()
    {
        playerfollowCamera.transform.position = playerFollowCameraStartPosition;
        playerfollowCamera.PreviousStateIsValid = false;
    }
}
