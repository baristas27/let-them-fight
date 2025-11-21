using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    [Header("Base Stats")]
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    public bool IsAlive => currentHealth > 0f;

    private RagdollController ragdollController;
    private FighterAI ai;

    // triggered when this character dies
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        ragdollController = GetComponent<RagdollController>();
        ai = GetComponent<FighterAI>();
    }

    public void TakeDamage(DamageInfo info)
    {
        if (!IsAlive)
            return;

        float finalDamage = CalculateDamage(info);
        currentHealth -= finalDamage;
        Debug.Log($"{gameObject.name} took {finalDamage} {info.Type} damage from {info.Source.name}. Remaining HP: {currentHealth}");
        if(currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
    }

    private float CalculateDamage(DamageInfo info)
    {
        return info.Amount;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        
        if(ai != null)
        {
            ai.enabled = false;
        }


        if(ragdollController != null)
        {
            ragdollController.ToggleRagdoll(true);
        }

        OnDeath?.Invoke();
    }

}
