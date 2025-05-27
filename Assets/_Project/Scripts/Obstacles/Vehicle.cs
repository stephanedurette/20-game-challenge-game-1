using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;

    [Header("Settings")]
    [SerializeField] private List<GameObject> models;

    void Start()
    {
        Initialize(0);
    }

    public void Initialize(float moveSpeed)
    {
        rigidBody.linearVelocity = transform.forward * moveSpeed;
        SetRandomModel();
    }

    private void SetRandomModel()
    {
        int randIndex = Random.Range(0, models.Count);

        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(i == randIndex);
        }
    }
}
