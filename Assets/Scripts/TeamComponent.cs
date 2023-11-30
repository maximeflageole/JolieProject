using System.Collections.Generic;
using UnityEngine;

public class TeamComponent : MonoBehaviour
{
    [SerializeField]
    private bool m_isPlayerTeam;
    [SerializeField]
    private float m_unitDistance = 2.0f;
    [SerializeField]
    private List<Transform> m_unitSlots = new List<Transform>();
    public List<AutoBattlerUnit> TeamUnits { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        TeamUnits = new List<AutoBattlerUnit>();

        var i = 0;
        var units = GetComponentsInChildren<AutoBattlerUnit>();

        foreach (var unitSlot in m_unitSlots)
        {
            if (units.Length > i && units[i].enabled == true)
            {
                TeamUnits.Add(units[i]);
                units[i].SetTeam(m_isPlayerTeam);
            }

            var sign = 1;
            if (m_isPlayerTeam)
                sign = -1;
            unitSlot.transform.Translate(sign * i * m_unitDistance, 0f, 0f);
            i++;
        }
    }

    protected void Update()
    {
        if (!BattleManager.Instance.InBattle) return;

        foreach (var unit in TeamUnits)
        {
            unit.UpdateBattle();
        }
    }

    private bool IsWiped()
    {
        return TeamUnits.Count == 0;
    }

    public void ChangeUnitPosition(AutoBattlerUnit unit, int index)
    {
        TeamUnits[index] = unit;
        unit.m_positionIndex = index;
        unit.transform.SetParent(m_unitSlots[index].transform);
        //TODO: Temporary teleport to the correct position. Slide there smoothly
        unit.transform.localPosition = new Vector3(0, 0, 0);
    }
    public void OnUnitDeath(AutoBattlerUnit unit)
    {
        var unitPos = unit.m_positionIndex;
        TeamUnits.RemoveAt(unitPos);

        for (var i = unitPos; i < TeamUnits.Count; i++)
        {
            if (TeamUnits[i] != null)
                ChangeUnitPosition(TeamUnits[i], i - 1);
        }

        Debug.Log("Team " + gameObject.name + " has lost a member. Current team size is now: " + TeamUnits.Count);

        if (IsWiped())
        {
            BattleManager.Instance.OnBattleEnded(this);
        }
    }
}
