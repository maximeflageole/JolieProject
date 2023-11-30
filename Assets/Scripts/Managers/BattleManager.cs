using MoreMountains.Feedbacks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    public bool InBattle { get; private set; } = true;
    [field:SerializeField]
    public TurnOrderLayout TurnOrderLayout { get; private set; }
    [SerializeField]
    protected MMFeedbacks m_timeSlowFeedback;
    public List<TurnOrder> TurnsOrder { get; private set; } = new List<TurnOrder>();
    [SerializeField]
    private TextMeshProUGUI m_battleResultTxt;

    [field: SerializeField]
    public TeamComponent PlayerTeam { get; private set; }
    [field: SerializeField]
    public TeamComponent EnemyTeam { get; private set; }

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

        OnRefreshTurnOrder();
    }

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
        TeamComponent team = EnemyTeam;

        if (unit.m_isPlayerTeam)
            team = PlayerTeam;

        team.OnUnitDeath(unit);
    }

    public void OnRefreshTurnOrder()
    {
        return;

        TurnsOrder.Clear();
        //Iterate through every unit to find which one attacks next
        //Repeat everytime this is called for EVERY position. This might be called before the cast speed of units has changed

        List<TurnOrder> tempList = new List<TurnOrder>();

        foreach (var unit in PlayerTeam.TeamUnits)
        {
            tempList.Add(new TurnOrder(unit, unit.AbilitiesExecutionTime[0], 0));
        }
        foreach (var unit in EnemyTeam.TeamUnits)
        {
            tempList.Add(new TurnOrder(unit, unit.AbilitiesExecutionTime[0], 0));
        }

        for (int i = 1; i < 12 /* todo const */; i++)
        {
            var lowestAbilityTime = tempList[0];

            foreach (var tempTurnOrder in tempList)
            {
                if (lowestAbilityTime.AbilityTime > tempTurnOrder.AbilityTime)
                {
                    lowestAbilityTime = tempTurnOrder;
                }
            }

            TurnsOrder.Add(lowestAbilityTime);
            tempList.Remove(lowestAbilityTime);
            tempList.Add(new TurnOrder(lowestAbilityTime.Unit, 
                lowestAbilityTime.Unit.AbilitiesExecutionTime[lowestAbilityTime.AbilityIndex + 1], 
                lowestAbilityTime.AbilityIndex + 1));
        }

        List<Sprite> turnOrderSprites = new List<Sprite>();
        foreach (var unit in TurnsOrder)
        {
            turnOrderSprites.Add(unit.Unit.UnitData.Sprite);
        }
        TurnOrderLayout.PopulateImages(turnOrderSprites);
    }

    public void OnUnitAttack()
    {
        m_timeSlowFeedback.StopFeedbacks(true);
        m_timeSlowFeedback.PlayFeedbacks();
    }

    public void OnBattleEnded(TeamComponent team)
    {
        InBattle = false;
        m_battleResultTxt.gameObject.SetActive(true);
        if (team == EnemyTeam)
        {
            m_battleResultTxt.text = "Victory!";
            return;
        }
        m_battleResultTxt.text = "Defeat";
    }
}

public struct TurnOrder
{
    public TurnOrder(AutoBattlerUnit unit, float abilityTime, int abilityIndex)
    {
        Unit = unit;
        AbilityTime = abilityTime;
        AbilityIndex = abilityIndex;
    }

    public AutoBattlerUnit Unit;
    public float AbilityTime;
    public int AbilityIndex;
}