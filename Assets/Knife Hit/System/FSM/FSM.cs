using UnityEngine;

public class FSM : MonoBehaviour {
    public FSMState inicialState;
    public FSMState currentState;

    private void Start() {
        ChangeState(inicialState);
    }

    public void ChangeState(FSMState newState){
        if(currentState != null){
            currentState.StateExit();
        }

        currentState = newState;
        if(currentState != null){
            currentState.fsm = this;
            currentState.StateEnter();
        }else{
            Debug.LogWarning("You're trying to change fsm to an empty state.");
        }
    }

    private void Update() {
        if(currentState != null){
            if(!currentState.useFixedUpdate){
                currentState.StateExecute();
            }
        }
    }

    private void FixedUpdate() {
        if(currentState != null){
            if(currentState.useFixedUpdate){
                currentState.StateExecute();
            }
        }
    }
}