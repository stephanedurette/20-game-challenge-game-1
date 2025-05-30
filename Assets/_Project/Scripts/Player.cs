using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private float sidewaysAccel;
    [SerializeField] private float sidewaysMaxSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform rotatePivot;

    private int currentDirection = -1;

    private void Start()
    {
        rigidBody.linearVelocity = Vector3.forward * forwardMoveSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rigidBody.linearVelocity;

        velocity.x = Mathf.Clamp(velocity.x + currentDirection * sidewaysAccel * Time.fixedDeltaTime, -sidewaysMaxSpeed, sidewaysMaxSpeed);

        rigidBody.linearVelocity = velocity;

        rotatePivot.forward = rigidBody.linearVelocity.normalized;
    }

    public void OnChangeDirection(CallbackContext ctx)
    {
        if (ctx.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            currentDirection *= -1;
        }
    }

    public void OnCollide()
    {
        
    }
}
