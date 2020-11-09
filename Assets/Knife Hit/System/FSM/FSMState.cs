using UnityEngine;

public abstract class FSMState : MonoBehaviour {
    public FSM fsm;
    public bool useFixedUpdate = false;

    public abstract void StateEnter();
    public abstract void StateExecute();
    public abstract void StateExit();
}