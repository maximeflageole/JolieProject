using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    public float Health = 100.0f;
    public float AttackDamage = 10.0f;
    public float AttackSpeed = 5.0f;
    public EAttackType AttackType = EAttackType.Melee;
    public AbilityData AbilityData;
    public Sprite Sprite;
}

public enum EAttackType
{
    Melee,
    RangedMirror,
    Default
}