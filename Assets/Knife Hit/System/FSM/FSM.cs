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
        if(newState != null){
            newState.StateEnter();
            newState.fsm = this;
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