using System;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private string initState;
    [SerializeField] private FSMState[] states;
    public FSMState CurrentState {get; set;}

    private void Start()
    {
        ChangeState(initState);
    }

    private void Update()
    {
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateId)
    {
        FSMState newState = GetState(newStateId);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSMState GetState(string newStateId)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if(states[i].iD == newStateId)
                return states[i];
        }
        return null;
    }
}
