using UnityEngine;

public class DamageTester : MonoBehaviour
{
    [Header("Target to Damage")]
    public HealthSystem target;

    [Header("Damage Settings")]
    public float damageAmount = 25f;
    public DamageType damageType = DamageType.Physical;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (target == null)
            {
                Debug.LogWarning("Target not assigned!");
                target = FindObjectOfType<HealthSystem>();
            }

            DamageInfo info = new DamageInfo(
                damageAmount,
                target.transform.position,
                gameObject,
                damageType
                );

            target.TakeDamage(info);
        }
    }
}
