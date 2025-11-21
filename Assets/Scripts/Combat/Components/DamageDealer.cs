using UnityEngine;


[RequireComponent(typeof(Collider))]
public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private DamageType damageType = DamageType.Physical;

    [Tooltip("if true, deals damage once per contact; otherwise deals continous damage.")]
    [SerializeField] private bool singleHit = true;

    [Tooltip("delay between continous damage applications(seconds).")]
    [SerializeField] private float damageInterval = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage(collision.gameObject, collision.contacts[0].point);
    }

    private void OnTriggerEnter(Collider other)
    {
        DealDamage(other.gameObject, other.ClosestPoint(transform.position));
    }

    /// <summary>
    ///  attempts to deal damage to a target GameObject
    ///  Can be called manually by AI (AttackingState) or passively be physics (OnCollisionEnter)
    /// </summary>
    /// <param name="targetObject">The GameObject to receive damage</param>
    /// <param name="hitpoint">The world position where the hit occured.</param>



    public void DealDamage(GameObject targetObject, Vector3 hitpoint)
    {
        if (targetObject == null) return;

        var damageable = targetObject.GetComponent<IDamageable>();
        if (damageable == null) return;

        DamageInfo info = new DamageInfo(damageAmount, hitpoint, gameObject, damageType);

        damageable.TakeDamage(info);
    }
}
