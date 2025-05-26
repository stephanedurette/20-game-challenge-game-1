using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityUtils;

[RequireComponent(typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    public UnityEvent<GameObject> OnCollisionEnter;
    public UnityEvent<GameObject> OnCollisionStay;
    public UnityEvent<GameObject> OnCollisionExit;

    private Collider col;
    private Rigidbody rigidBody;
    private List<Collider> colliders;

    public T[] GetCollidingObjects<T>()
    {
        Collider[] cols = colliders.Where((col) => col.gameObject.TryGetComponent(out T _)).ToArray();
        T[] components = cols.Select((col) => col.gameObject.GetComponent<T>()).ToArray();

        return components;
    }

    public T GetCollidingObject<T>()
    {
        return GetCollidingObjects<T>().FirstOrDefault();
    }

    public bool IsColliding => colliders.Count > 0;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col.isTrigger = true;
        rigidBody.isKinematic = true;
        colliders = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collisionMask.Contains(other.gameObject.layer)) return;

        if (!colliders.Contains(other))
            colliders?.Add(other);
        
        OnCollisionEnter?.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!collisionMask.Contains(other.gameObject.layer)) return;

        OnCollisionStay?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collisionMask.Contains(other.gameObject.layer)) return;

        colliders?.Remove(other);
        OnCollisionExit?.Invoke(other.gameObject);
    }


}
