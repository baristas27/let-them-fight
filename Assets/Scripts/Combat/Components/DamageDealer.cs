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
        TryDealDamage(collision.gameObject, collision.contacts[0].point);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDealDamage(other.gameObject, other.ClosestPoint(transform.position));
    }

    private void TryDealDamage(GameObject targetObject,  Vector3 hitPoint)
    {
        var damageable = targetObject.GetComponent<IDamageable>();
        if (damageable == null)
            return;

        DamageInfo info = new DamageInfo(damageAmount, hitPoint, gameObject, damageType);
        damageable.TakeDamage(info);
    }
}
