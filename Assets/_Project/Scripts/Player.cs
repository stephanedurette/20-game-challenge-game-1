using ImprovedTimers;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float forwardMoveSpeed;
    [SerializeField] private float sidewaysAccel;
    [SerializeField] private float sidewaysMaxSpeed;
    [SerializeField][Range(0, 1)] private float driftTolerance;
    [SerializeField][Range(0, 1)] private float driftTime;

    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform rotatePivot;

    [Header("Events")]
    [SerializeField] private UnityEvent OnDirectionChange;
    [SerializeField] private UnityEvent OnDriftStart;
    [SerializeField] private UnityEvent OnDriftStop;

    private int currentDirection = -1;
    private Vector3 velocity;
    private CountdownTimer driftTimer;

    private void Awake()
    {
        driftTimer = new CountdownTimer(driftTime);
        driftTimer.OnTimerStart += () => { OnDriftStart?.Invoke(); };
        driftTimer.OnTimerStop += () => { OnDriftStop?.Invoke(); };
    }

    private void Start()
    {
        rigidBody.linearVelocity = Vector3.forward * forwardMoveSpeed;
    }

    private void FixedUpdate()
    {
        velocity = rigidBody.linearVelocity;

        velocity.x = Mathf.Clamp(velocity.x + currentDirection * sidewaysAccel * Time.fixedDeltaTime, -sidewaysMaxSpeed, sidewaysMaxSpeed);

        rigidBody.linearVelocity = velocity;

        rotatePivot.forward = rigidBody.linearVelocity.normalized;
    }

    public void OnChangeDirection(CallbackContext ctx)
    {
        if (ctx.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            currentDirection *= -1;
            OnDirectionChange?.Invoke();
            
            if (driftTimer.IsRunning) { 
                driftTimer.Stop();
            }

            if (Mathf.Abs(velocity.x) > sidewaysMaxSpeed * driftTolerance)
            {
                driftTimer.Reset();
            }
            
        }
    }

    

    public void OnCollide()
    {
        
    }
}
