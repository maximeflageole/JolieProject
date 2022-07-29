using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Start()
    {
        Instance = this;

        int index = 0;
        foreach (var unit in PlayerTeam.TeamUnits)
        {
            unit.m_positionIndex = index;
            index++;
        }
        index = 0;
        foreach (var unit in EnemyTeam.TeamUnits)
        {
            unit.m_positionIndex = index;
            index++;
        }
    }

    [field:SerializeField]
    public TeamComponent PlayerTeam { get; private set; }
    [field: SerializeField]
    public TeamComponent EnemyTeam { get; private set; }

    public List<AutoBattlerUnit> GetTeam(bool isPlayerTeam, bool adverseTeam = false)
    {
        if (isPlayerTeam && adverseTeam || !isPlayerTeam && !adverseTeam)
        {
            return EnemyTeam.TeamUnits;
        }

        return PlayerTeam.TeamUnits;
    }

    public AutoBattlerUnit GetFrontEnemy(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            return EnemyTeam.TeamUnits[0];
        }
        return PlayerTeam.TeamUnits[0];
    }

    public AutoBattlerUnit GetMirroredEnemy(bool isPlayerTeam, int positionIndex)
    {
        List<AutoBattlerUnit> team = null;

        if (isPlayerTeam)
        {
            team = EnemyTeam.TeamUnits;
        }
        else
        {
            team = PlayerTeam.TeamUnits;
        }

        if (team.Count > positionIndex)
            return team[positionIndex];

        //If no unit at the mirrored index, get the unit that is at the end
        return team[team.Count - 1];
    }

    public void OnUnitDeath(AutoBattlerUnit unit)
    {
        TeamComponent team = null;
        if (unit.m_isPlayerTeam)
        {
            team = PlayerTeam;
        }
        else
        {
            team = EnemyTeam;
        }

        var unitPos = unit.m_positionIndex;
        for (var i = unitPos + 1; i < 5; i++)
        {
            if (team.TeamUnits[i] != null)
                team.ChangeUnitPosition(team.TeamUnits[i], i - 1);
        }
    }
}