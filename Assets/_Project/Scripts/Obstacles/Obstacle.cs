using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<GameObject> models;

    [Header("References")]
    [SerializeField] private Collider viewCollider;

    private bool enteredScreen;

    bool OnScreen => GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), viewCollider.bounds);

    public void Initialize()
    {
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
        }
        else if (enteredScreen)
        {
            gameObject.SetActive(false);
            //rigidBody.interpolation = RigidbodyInterpolation.None;
        }
    }
}
