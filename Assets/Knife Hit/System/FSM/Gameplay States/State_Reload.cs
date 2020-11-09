using UnityEngine;

public class State_Reload : FSMState
{
    public GameplayController gameplay;
    public FSMState next;

    public override void StateEnter()
    {
        Debug.Log("Reloading knife...");
        gameplay.DrawFirstKnife();
        gameplay.SetInitOk(true); //Communicate that gameplay is ready
        gameplay.playerControl = true; //Allow player to control the game

        fsm.ChangeState(next);
    }



    public override void StateExecute()
    {

    }

    public override void StateExit()
    {

    }
}