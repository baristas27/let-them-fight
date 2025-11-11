using UnityEngine;

public class AttackingState : IFighterState
{
    private FighterAI ai;


    public void Enter(FighterAI ai)
    {
        this.ai = ai;

        Debug.Log("entering attacking state");
    }
    
    public void Execute()
    {
        Debug.Log("I'm attacking...");
    }


    public void Exit()
    {
        Debug.Log("I've quit attacking state...");
    }


}
