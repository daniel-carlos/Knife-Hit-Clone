using UnityEngine;

public class State_Preparation : FSMState
{
    public GameplayController gameplay;
    
    public override void StateEnter()
    {
        gameplay.LoadCurrentLevel();
        gameplay.FillLogWithStartingKnifes();
        gameplay.SetInitOk(true); //Communicate that gameplay is ready
        gameplay.playerControl = true; //Allow player to control the game
    }

    public override void StateExecute()
    {
        
    }

    public override void StateExit()
    {
        
    }
}