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
    [SerializeField]private float driftTime;
    [SerializeField] private float driftDelayTime;

    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform rotatePivot;
    [SerializeField] private TrailRenderer[] skidMarks;

    [Header("Events")]
    [SerializeField] private UnityEvent OnDirectionChange;
    [SerializeField] private UnityEvent OnDriftStart;
    [SerializeField] private UnityEvent OnDriftStop;
    [SerializeField] private UnityEvent OnCollided;

    private int currentDirection = -1;
    private Vector3 velocity;
    private CountdownTimer driftTimer;
    private CountdownTimer driftDelayTimer;

    private Vector3 startingPosition;

    private void Awake()
    {
        startingPosition = transform.position;

        driftTimer = new CountdownTimer(driftTime);
        driftTimer.OnTimerStart += () => { OnDriftStart?.Invoke(); };
        driftTimer.OnTimerStop += () => { OnDriftStop?.Invoke(); };

        driftDelayTimer = new CountdownTimer(driftDelayTime);
        driftDelayTimer.OnTimerStop += () => driftTimer.Start();
    }

    private void Start()
    {
        //rigidBody.linearVelocity = Vector3.forward * forwardMoveSpeed;
        //ToggleSkidMarks(false);
    }

    public void ResetToStart()
    {
        transform.position = startingPosition;
        transform.rotation = Quaternion.identity;
        currentDirection = -1;
        rigidBody.linearVelocity = Vector3.forward * forwardMoveSpeed;
        foreach(var s in skidMarks)
        {
            s.Clear();
        }
        gameObject.SetActive(true);
        ToggleSkidMarks(false);
    }

    public void ToggleSkidMarks(bool on)
    {
        foreach(var skidMark in skidMarks)
        {
            skidMark.emitting = on;
        }
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
                driftDelayTimer.Start();
            }
            
        }
    }

    

    public void OnCollide()
    {
        gameObject.SetActive(false);
        OnCollided?.Invoke();
    }
}
