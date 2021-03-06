using UnityEngine;

public class State_Preparation : FSMState
{
    public GameplayController gameplay;
    public FSMState next;
    
    public override void StateEnter()
    {
        Debug.Log("Preparing Level");
        gameplay.LoadCurrentLevel();
        gameplay.FillLogWithStartingKnifes();

        fsm.ChangeState(next);
    }

    public override void StateExecute()
    {
        
    }

    public override void StateExit()
    {
        
    }
}