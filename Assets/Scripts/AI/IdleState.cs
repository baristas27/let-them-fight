using System.Security.Cryptography;
using UnityEngine;

public class IdleState : IFighterState
{
    private FighterAI ai;

    /// <summary>
    /// called when entering the idlestate
    /// ensures the character stops all movement
    /// </summary>
    public void Enter(FighterAI ai)
    {
        this.ai = ai;

        if(ai.Rb != null )
        {
            ai.Rb.linearVelocity = new Vector3(0, ai.Rb.linearVelocity.y, 0);
        }
        Debug.Log("entering idle state");
    }

    public void Execute()
    {

    }

    /// <summary>
    /// called when exiting the idlestate
    /// </summary>
    public void Exit()
    {
        Debug.Log("exiting idle state");   
    }


}
