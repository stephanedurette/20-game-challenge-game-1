using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool xAxisFollow;
    [SerializeField] private bool yAxisFollow;
    [SerializeField] private bool zAxisFollow;

    private Vector3 offset;

    private void Awake()
    {
        offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            xAxisFollow ? offset.x + target.position.x : transform.position.x,
            yAxisFollow ? offset.y + target.position.y : transform.position.y,
            zAxisFollow ? offset.z + target.position.z : transform.position.z
        );
    }
}
