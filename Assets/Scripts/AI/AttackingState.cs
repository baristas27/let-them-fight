using Unity.VisualScripting;
using UnityEngine;

public class AttackingState : IFighterState
{
    private FighterAI ai;
    private float attackTimer;

    /// <summary>
    /// Called when entering the AttackingState
    /// Resets the attack timer to fire the first attack immediately
    /// </summary>
    /// <param name="ai"></param>


    public void Enter(FighterAI ai)
    {
        this.ai = ai;

        ToggleArmPhysics(true);
    }


    /// <summary>
    /// called every frame while in the AttackingnState
    /// checks for exit conditions and handles the attack timer logic
    /// </summary>
  
    public void Execute()
    {
        if (ai.Target == null || !ai.Target.GetComponent<IDamageable>().IsAlive)
        {
            ai.ChangeState(ai.idleState);
            return;
        }

        float distance = Vector3.Distance(ai.transform.position, ai.Target.position);
        if(distance > ai.chaseRange)
        {
            ai.ChangeState(ai.movingState);
            return;
        }

        if(ai.weaponArm !=null)
        {
            Vector3 direction =(ai.Target.position - ai.transform.position).normalized;
            Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);



            if(lookDirection != Vector3.zero)
            { 
            
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, targetRotation, Time.deltaTime * 5f);
            }

            if(ai.Target != null && ai.weaponTip != null)
            {
                Vector3 targetPoint = ai.Target.position - ai.transform.position + (Vector3.up * 1.0f);

                Vector3 attackDir = (targetPoint - ai.weaponTip.position).normalized;

                ai.weaponArm.AddForce(attackDir * ai.attackForce * Time.deltaTime, ForceMode.VelocityChange);

                ai.Rb.angularVelocity = Vector3.Lerp(ai.Rb.angularVelocity, Vector3.zero, Time.deltaTime * 2f);
            }
        }
    }

    /// <summary>
    /// called when exiting AttackState
    /// </summary>
    public void Exit()
    {
        ToggleArmPhysics(false);
        Debug.Log("I've quit attacking state...");
    }

    /// <summary>
    /// handles the actual damage logic by delegating the call
    /// to the "DamageDealer" component on our "Brain"
    /// </summary>
    private void PerformAttack()
    {
        ai.DamageDealer.DealDamage(ai.Target.gameObject, ai.Target.position);
        Debug.Log($"{ai.name} ATTACKED! (Target: {ai.Target.name})");
    }

    private void ToggleArmPhysics(bool isPhysical)
    {
        if (ai.weaponArm == null) return;

        ai.weaponArm.isKinematic = !isPhysical;

        if(!isPhysical)
        {
            ai.weaponArm.linearVelocity = Vector3.zero;
            ai.weaponArm.angularVelocity = Vector3.zero;
        }

        Rigidbody[] allChildRbs = ai.weaponArm.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rb in allChildRbs)
        {
            if(rb == ai.weaponArm) continue;
            rb.isKinematic = !isPhysical;

            if(!isPhysical)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

}
