using UnityEngine;
using UnityEngine.UI;

public class State_Defeat : FSMState
{
    public GameplayController gameplay;
    public FSMState next;

    public GameObject defeatScreen;

    public override void StateEnter()
    {
        Debug.Log("Show Defeat Menu");
        defeatScreen.SetActive(true);

        gameplay.currentLog.StopRotating();
    }

    void OnDefeat()
    {

    }
    
    void OnKnifeHitLog()
    {

    }

    public override void StateExecute()
    {

    }

    public override void StateExit()
    {

    }
}