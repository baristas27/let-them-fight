using UnityEngine;

public struct DamageInfo
{
    // How much damage is being dealt
    public float Amount;


    // The wworld position where the hit occured
    public Vector3 HitPoint;

    // The object that caused the damage
    public GameObject Source;

    // The type of the damage
    public DamageType Type;

    // Constructur for eazy initialazation
    // DamageType.Physical == Default damage type
    public DamageInfo(float amount, Vector3 hitPoint, GameObject source,DamageType type = DamageType.Physical)
    {
        Amount = amount;
        HitPoint = hitPoint;
        Source = source;
        Type = type;
    }
}
