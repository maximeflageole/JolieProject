using UnityEngine;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Start()
    {
        Instance = this;
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
}