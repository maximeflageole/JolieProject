using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    public float m_health = 100.0f;
    public float m_attackDamage = 10.0f;
    public float m_attackSpeed = 5.0f;
    public EAttackType m_attackType = EAttackType.Melee;
    public float m_abilityCooldown = 10.0f;
    public AbilityData m_abilityData;
}

public enum EAttackType
{
    Melee,
    Ranged,
    Default
}