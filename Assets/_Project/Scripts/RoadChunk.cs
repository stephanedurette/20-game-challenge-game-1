using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RoadChunk otherChunk;
    [SerializeField] private Transform otherChunkSpawnPosition;

    private Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;
    }

    public void ResetChunk()
    {
        transform.position = startingPosition;
        gameObject.SetActive(true);
    }

    public void OnPlayerCollide()
    {
        otherChunk.transform.position = otherChunkSpawnPosition.position;
    }
}
