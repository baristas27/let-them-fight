using JetBrains.Annotations;
using UnityEngine;

public class RuntimeCharacter : MonoBehaviour
{
    [Header("Definition (ScriptableObject asset)")]

    public CharacterDefinition definition;

    [Header("Runtime Stats")]
    public float currentHealth;
    public float attackDamage;
    public float defense;
    public float attackRate;
    public float moveSpeed;
    public float fireRes, poisonRes;

    private void Awake()
    {
        //if (definition != null)
        //{
        //    ApplyDefinition(definition);
        //}
    }

    public void ApplyDefinition(CharacterDefinition def)
    {
        if(def == null)
        {
            Debug.LogWarning("ApplyDefinition: Taným (CharacterDefinition) bulunamadý.");
            return;
        }

        definition = def;


        currentHealth = def.maxHealth;
        attackDamage = def.attackDamage;
        defense = def.defense;
        attackRate = def.attackRate;
        moveSpeed = def.moveSpeed;
        fireRes = def.fireRes;
        poisonRes = def.poisonRes;

        Debug.Log($"ApplyDefinition: {def.displayName} yüklendi (HP={currentHealth}, DMG={attackDamage})");

    }
}
