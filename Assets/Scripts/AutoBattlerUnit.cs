using UnityEngine;
using UnityEngine.UI;

public class AutoBattlerUnit : MonoBehaviour
{
    [SerializeField]
    protected Image m_attackBar;
    [SerializeField]
    protected float m_attackSpeed = 2f;
    protected float m_currentAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_currentAttackTime += Time.deltaTime;
        if (m_currentAttackTime > m_attackSpeed)
        {
            m_currentAttackTime %= m_attackSpeed;
            PerformAttack();
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (m_attackBar == null) return;

        m_attackBar.fillAmount = m_currentAttackTime / m_attackSpeed;
    }

    private void PerformAttack()
    {

    }
}
