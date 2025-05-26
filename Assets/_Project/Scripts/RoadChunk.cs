using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoadChunk otherChunk;
    [SerializeField] private Transform otherChunkSpawnPosition;

    public void OnPlayerCollide()
    {
        otherChunk.transform.position = otherChunkSpawnPosition.position;
    }
}
