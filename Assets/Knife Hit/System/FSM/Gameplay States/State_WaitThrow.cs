using UnityEngine;

public class State_WaitThrow : FSMState
{
    public GameplayController gameplay;
    public FSMState reloadState;
    public FSMState victoryState;
    public FSMState defeatState;

    public override void StateEnter()
    {
        Debug.Log("Waiting for player");
    }

    public void OnThrowKnife()
    {
        Debug.Log("A kife has throwed.");
        fsm.ChangeState(reloadState);
    }
    public void OnDefeat()
    {
        fsm.ChangeState(defeatState);
    }

    //Called by GameplayController via SendMessage
    public void OnKnifeHitLog()
    {
        if (gameplay.knifesContainer.childCount == 0)
        {
            fsm.ChangeState(victoryState);
        }
    }

    public override void StateExecute()
    {

    }

    public override void StateExit()
    {
        gameplay.playerControl = false;
        gameplay.SetInitOk(false);
    }
}