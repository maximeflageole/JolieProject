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
    protected AbilityData m_abilityData;
    [SerializeField]
    protected int m_maxHealth = 20;

    protected int m_currentHealth;
    protected float m_currentAttackTime = 0f;

    private void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    void Update()
    {
        m_currentAttackTime += Time.deltaTime;
        if (m_currentAttackTime > m_abilityData.AbilityCooldown)
        {
            m_currentAttackTime %= m_abilityData.AbilityCooldown;
            PerformAbility();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_attackBar?.UpdateBar(m_currentAttackTime / m_abilityData.AbilityCooldown);
        m_lifeBar?.UpdateBar((float)m_currentHealth / m_maxHealth);
    }

    private void PerformAbility()
    {
        AbilitiesManager.SolveAbility(m_abilityData, this);
    }

    public void ReceiveDamage(int magnitude)
    {
        m_currentHealth -= magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
    }

    public void HealDamage(int magnitude)
    {
        m_currentHealth += magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, m_maxHealth);
    }
}
