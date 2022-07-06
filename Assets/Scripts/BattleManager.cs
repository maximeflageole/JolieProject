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
    public List<AutoBattlerUnit> PlayerUnits { get; private set; }
    [field: SerializeField]
    public List<AutoBattlerUnit> EnemyUnits { get; private set; }
}