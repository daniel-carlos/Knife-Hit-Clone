using UnityEngine;

public class State_Victory : FSMState
{
    public GameplayController gameplay;
    public float delayTime = 1f;
    public FSMState preparationState;

    public override void StateEnter()
    {
        Debug.Log("Congratulations");
        gameplay.currentLog.BreakLog();
        Invoke("BreakCurrentLog", delayTime);
    }

    void BreakCurrentLog()
    {
        if (gameplay.NextLevel())
        {
            fsm.ChangeState(preparationState);
        }else{
            gameplay.EndGame();
        }
    }

    public override void StateExecute()
    {

    }

    public override void StateExit()
    {

    }
}