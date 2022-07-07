using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Start()
    {
        Instance = this;

        foreach (var unit in PlayerUnits)
        {
            unit.m_isPlayerTeam = true;
        }
        foreach (var unit in EnemyUnits)
        {
            unit.m_isPlayerTeam = false;
        }
    }

    [field:SerializeField]
    public List<AutoBattlerUnit> PlayerUnits { get; private set; }
    [field: SerializeField]
    public List<AutoBattlerUnit> EnemyUnits { get; private set; }

    public List<AutoBattlerUnit> GetTeam(bool isPlayerTeam, bool adverseTeam = false)
    {
        if (isPlayerTeam && adverseTeam || !isPlayerTeam && !adverseTeam)
        {
            return EnemyUnits;
        }

        return PlayerUnits;
    }
}