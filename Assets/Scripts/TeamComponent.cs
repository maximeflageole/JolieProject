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
        foreach (var unit in GetComponentsInChildren<AutoBattlerUnit>())
        {
            TeamUnits.Add(unit);
            unit.m_isPlayerTeam = m_isPlayerTeam;
            var sign = 1;
            if (m_isPlayerTeam)
                sign = -1;
            m_unitSlots[i].Translate(sign * i * m_unitDistance, 0f, 0f);
            i++;
        }
    }

    public void ChangeUnitPosition(AutoBattlerUnit unit, int index)
    {
        TeamUnits[index] = unit;
        TeamUnits[index + 1] = null;
        unit.m_positionIndex = index;
        unit.transform.SetParent(m_unitSlots[index]);
        //TODO: Temporary teleport to the correct position. Slide there smoothly
        unit.transform.localPosition = new Vector3(0, 0, 0);
    }
}
