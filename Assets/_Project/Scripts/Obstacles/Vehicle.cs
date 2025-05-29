using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Collider viewCollider;

    [Header("Settings")]
    [SerializeField] private List<GameObject> models;

    private bool enteredScreen;

    bool OnScreen => GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), viewCollider.bounds);

    public void Initialize(float moveSpeed, Vector3 position)
    {
        rigidBody.linearVelocity = transform.forward * moveSpeed;

        rigidBody.MovePosition(position); //extrapolate messes with the spawning position unless it's set by the rigidbody
        SetRandomModel();
        enteredScreen = false;
    }

    private void SetRandomModel()
    {
        int randIndex = Random.Range(0, models.Count);

        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(i == randIndex);
        }
    }
    
    private void FixedUpdate()
    {
        if (OnScreen)
        {
            enteredScreen = true;
        } else if (enteredScreen)
        {
            gameObject.SetActive(false);
            //rigidBody.interpolation = RigidbodyInterpolation.None;
        }
    }
    
}
