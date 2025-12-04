using UnityEngine;

public class StateSwapperTriggerArea : MonoBehaviour
{

    public EnemyState stateOnEnterArea;
    public EnemyState stateOnExitArea;
    private EnemyStateMachine stateMachine;

    public float timeToLockOff = 10f;
    private float internalTimer;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        
        if (_other.CompareTag("Player") && stateMachine)
        {
            //Debug.Log("Player Entered Turret Area");
            internalTimer = timeToLockOff + 1f;
            stateMachine.ChangeState(stateOnEnterArea.stateId);
        }
    }

    public void Update()
    {
        if (internalTimer <= timeToLockOff)
        {
            internalTimer -= Time.deltaTime;
        }
        
        if (internalTimer <= 0)
        {
            internalTimer = timeToLockOff + 1f; // Sets the timer above timeToLockOff, making it fail the if statement on line 23
            //Debug.Log("LockOff Timer Ended");
            stateMachine.ChangeState(stateOnExitArea.stateId);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {


        if (_other.CompareTag("Player") && stateMachine)
        {
            //Debug.Log("LockOff Timer Started");
            internalTimer = timeToLockOff;
        }
    }
}
