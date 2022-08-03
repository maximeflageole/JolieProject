using UnityEngine;
using AbilitySystem;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

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

    [SerializeField]
    protected MMFeedbacks m_attackFeedback; 

    protected float m_currentHealth;
    protected float m_currentAttackTime = 0f;
    protected float m_currentAbilityTime = 0f;

    public List<float> AbilitiesExecutionTime;

    private void Awake()
    {
        AbilitiesExecutionTime = new List<float>();

        var cooldown = UnitData.AbilityData.AbilityCooldown;
        var nextAttackTimer = cooldown;

        for (int i = 0; i < 16; i++)
        {
            AbilitiesExecutionTime.Add(nextAttackTimer);
            nextAttackTimer += cooldown;
        }
    }

    private void Start()
    {
        m_currentHealth = UnitData.Health;
    }

    public void SetTeam(bool playerTeam)
    {
        m_isPlayerTeam = playerTeam;

        if (!playerTeam)
        {
            m_attackFeedback.FeedbacksIntensity = -1;
        }
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
        //TODO: Might remove the AA entirely. Should keep the targeting system it uses
        AbilitiesManager.SolveAutoAttack(this, UnitData.AttackDamage);
        m_attackFeedback.PlayFeedbacks();
    }

    private void PerformAbility()
    {
        AbilitiesExecutionTime.RemoveAt(0);
        var value = AbilitiesExecutionTime[AbilitiesExecutionTime.Count - 1] + UnitData.AbilityData.AbilityCooldown;
        AbilitiesExecutionTime.Add(value);
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
