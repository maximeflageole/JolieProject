using UnityEngine;
using AbilitySystem;

public class AutoBattlerUnit : MonoBehaviour
{
    public bool m_isPlayerTeam;
    public int m_positionIndex;

    [SerializeField]
    protected ChargeBar m_attackBar;
    [SerializeField]
    protected ChargeBar m_abilityBar;
    [SerializeField]
    protected ChargeBar m_lifeBar;
    [field:SerializeField]
    public UnitData UnitData { get; protected set; }

    protected float m_currentHealth;
    protected float m_currentAttackTime = 0f;
    protected float m_currentAbilityTime = 0f;


    private void Start()
    {
        m_currentHealth = UnitData.Health;
    }

    void Update()
    {
        m_currentAttackTime += Time.deltaTime;
        m_currentAbilityTime += Time.deltaTime;

        if (m_currentAttackTime > UnitData.AttackSpeed)
        {
            m_currentAttackTime %= UnitData.AttackSpeed;
            PerformAutoAttack();
        }
        if (m_currentAbilityTime > UnitData.AbilityData.AbilityCooldown)
        {
            m_currentAbilityTime %= UnitData.AbilityData.AbilityCooldown;
            PerformAbility();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        m_attackBar?.UpdateBar(m_currentAttackTime / UnitData.AttackSpeed);
        m_abilityBar?.UpdateBar(m_currentAbilityTime / UnitData.AbilityData.AbilityCooldown);
        m_lifeBar?.UpdateBar((float)m_currentHealth / UnitData.Health);
    }

    private void PerformAutoAttack()
    {
        AbilitiesManager.SolveAutoAttack(this, UnitData.AttackDamage);
    }

    private void PerformAbility()
    {
        AbilitiesManager.SolveAbility(UnitData.AbilityData, this);
    }

    public void ReceiveDamage(float magnitude)
    {
        m_currentHealth -= magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, UnitData.Health);
        if (m_currentHealth <= float.Epsilon)
            Die();
    }

    public void HealDamage(int magnitude)
    {
        m_currentHealth += magnitude;
        m_currentHealth = Mathf.Clamp(m_currentHealth, 0, UnitData.Health);
    }

    private void Die()
    {
        BattleManager.Instance.OnUnitDeath(this);
        //TODO: Do not disappear this brutally
        Destroy(gameObject);
    }
}
