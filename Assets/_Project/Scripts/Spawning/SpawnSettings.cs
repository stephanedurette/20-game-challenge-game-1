using System;
using UnityEngine;

public class SpawnSettings
{
    public GameObject SpawnPrefab;
    public Vector3 SpawnPosition;
    public Action<GameObject> SpawnAction;

    public SpawnSettings(GameObject SpawnPrefab, Vector3 SpawnPosition) { 
        this.SpawnPrefab = SpawnPrefab;
        this.SpawnPosition = SpawnPosition;
    }

    public SpawnSettings(GameObject SpawnPrefab, Vector3 SpawnPosition, Action<GameObject> SpawnAction)
    {
        this.SpawnPrefab = SpawnPrefab;
        this.SpawnPosition = SpawnPosition;
        this.SpawnAction = SpawnAction;
    }
}
