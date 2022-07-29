using AbilitySystem;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    public float m_health = 100.0f;
    public float m_attackDamage = 10.0f;
    public float m_attackSpeed = 5.0f;
    public float m_abilityCooldown = 10.0f;
    [SerializeField]
    protected AbilityData m_abilityData;
}
