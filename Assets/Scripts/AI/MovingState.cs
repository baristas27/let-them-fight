using UnityEngine;

public class MovingState : IFighterState
{
    private FighterAI ai;

    public void Enter(FighterAI ai)
    {
        this.ai = ai;
    }

    public void Execute()
    {
        if (ai.Target == null) return;

        float distance = Vector3.Distance(ai.transform.position, ai.Target.position);

        if(distance <= ai.attackRange)
        {
            ai.ChangeState(ai.attackingState);
            return;
        }



        Vector3 direction = (ai.Target.position - ai.transform.position).normalized;

        Vector3 moveVelocity = direction * ai.Stats.moveSpeed;

        ai.Rb.linearVelocity = new Vector3(moveVelocity.x, ai.Rb.linearVelocity.y, moveVelocity.z);



    }

    public void Exit()
    {
        if(ai.Rb != null)
        {
            ai.Rb.linearVelocity = new Vector3(0, ai.Rb.linearVelocity.y, 0);
        }
    }



}
