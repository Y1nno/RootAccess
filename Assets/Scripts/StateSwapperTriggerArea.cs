using UnityEngine;

public class StateSwapperTriggerArea : MonoBehaviour
{

    public EnemyState stateOnEnterArea;
    public EnemyState stateOnExitArea;
    
    private EnemyStateMachine stateMachine;

    [Tooltip("How long the state persists in tracking mode after the player leaves the targeting area")]
    public float timeToLockOff = 5f;
    

    private float internalLockOffTimer;
    



    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        
        if (_other.CompareTag("Player") && stateMachine)
        {
            //Debug.Log("Player Entered Turret Area");
            internalLockOffTimer = timeToLockOff + 1f;
            stateMachine.ChangeState(stateOnEnterArea.stateId);
        }
    }

    public void Update()
    {
        if (internalLockOffTimer <= timeToLockOff)
        {
            internalLockOffTimer -= Time.deltaTime;
        }
        
        if (internalLockOffTimer <= 0)
        {
            internalLockOffTimer = timeToLockOff + 1f; // Sets the timer above timeToLockOff, making it fail the conditional on line 23
            //Debug.Log("LockOff Timer Ended");
            stateMachine.ChangeState(stateOnExitArea.stateId);
        }
    }

    private void OnTriggerExit2D(Collider2D _other)
    {


        if (_other.CompareTag("Player") && stateMachine)
        {
            //Debug.Log("LockOff Timer Started");
            internalLockOffTimer = timeToLockOff;
        }
    }
}
