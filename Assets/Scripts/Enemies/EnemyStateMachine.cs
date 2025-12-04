using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [Header("States")]
    [Tooltip("Drop any number of EnemyState assets here.")]
    public List<EnemyState> stateAssets = new List<EnemyState>();

    [Tooltip("ID of the starting state (must match a state's stateId).")]
    public string startingStateId = "Patrol";

    [HideInInspector] public EnemyState currentState;

    // Lookup table: ID -> state
    private Dictionary<string, EnemyState> _stateLookup;

    [HideInInspector] public Transform player;
    

    private void Awake()
    {
        BuildLookup();
        
    }

    private void Start()
    {
        ChangeState(startingStateId);
        player = Player.Instance.gameObject.transform.GetChild(0).GetComponent<Transform>();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Tick(this);
        }
        
    }

    private void BuildLookup()
    {
        _stateLookup = new Dictionary<string, EnemyState>();

        foreach (var state in stateAssets)
        {
            if (state == null || string.IsNullOrEmpty(state.stateId))
                continue;

            if (!_stateLookup.ContainsKey(state.stateId))
            {
                _stateLookup.Add(state.stateId, state);
            }
            else
            {
                Debug.LogWarning($"Duplicate stateId '{state.stateId}' on {state.name}");
            }
        }
    }

    public void ChangeState(string newStateId)
    {
        if (string.IsNullOrEmpty(newStateId))
            return;

        if (!_stateLookup.TryGetValue(newStateId, out var newState))
        {
            Debug.LogWarning($"No state with ID '{newStateId}' found on {name}");
            return;
        }

        if (newState == currentState)
            return;

        if (currentState != null)
            currentState.Exit(this);

        currentState = newState;
        currentState.Enter(this);
    }
}
