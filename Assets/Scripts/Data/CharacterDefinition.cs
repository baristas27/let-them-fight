using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Character Definition", fileName = "NewCharacter")]
public class CharacterDefinition : ScriptableObject
{
    [Header("Identity")]
    public string displayName = "Fighter";
    public string classTag = "Balanced";

    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float defense = 5f;
    public float attackRate = 1.0f;
    public float moveSpeed = 3f;

    [Header("Resistances")]
    [Range(0, 1)] public float fireRes = 0f;
    [Range(0, 1)] public float poisonRes = 0f;
}
