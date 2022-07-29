using UnityEngine;
using AbilitySystem;

public class AutoBattlerUnit : MonoBehaviour
{
    public bool m_isPlayerTeam;

    [SerializeField]
    protected ChargeBar m_attackBar;
    [SerializeField]
    protected ChargeBar m_lifeBar;
    [SerializeField]
    protected UnitData m_unitData;

    protected float m_currentHealth;
    protected float m_currentAttackTime = 0f;

    private void Start()
    {
        m_currentHealth = m_unitData.m_health;
    }

    void Update()
    {
        m_currentAttackTime += Time.deltaTime;
        if (m_currentAttackTime > m_unitData.m_attackSpeed)
        {
            m_currentAttackTime %= m_unitData.m_attackSpeed;
            PerformAutoAttack();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_attackBar?.UpdateBar(m_currentAttackTime / m_unitData.m_attackSpeed);
        m_lifeBar?.UpdateBar((float)m_currentHealth / m_unitData.m_health);
    }

    private void PerformAutoAttack()
    {
        AbilitiesManager.SolveAutoAttack(this, m_unitData.m_attackDamage);
    }

    private void PerformAbility()
    {
        AbilitiesManager.SolveAbility(m_unitData.m_abilityData, this);
    }

    public void ReceiveDamage(float magnitude)
    {
        m_currentHealth -= magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_unitData.m_health);
    }

    public void HealDamage(int magnitude)
    {
        m_currentHealth += magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_unitData.m_health);
    }
}
