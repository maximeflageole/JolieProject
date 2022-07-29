using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public static class AbilitiesManager
    {
        public static void SolveAutoAttack(AutoBattlerUnit instigator, float damage)
        {
            //TODO: Now I am always targeting in melee range. Introduce other ranges
            var target = BattleManager.Instance.GetFrontEnemy(instigator.m_isPlayerTeam);
            target.ReceiveDamage(damage);
        }

        public static void SolveAbility(AbilityData abilityData, AutoBattlerUnit instigator)
        {
            foreach (var effect in abilityData.AbilityEffects)
            {
                var targets = GetEffectTargets(instigator, effect.TargetType, effect.TargetRange);

                SolveAbilityEffect(targets, effect);
            }
        }

        private static List<AutoBattlerUnit> GetEffectTargets(AutoBattlerUnit instigator, ETargetType targetType, ETargetRange targetRange)
        {
            var returnList = new List<AutoBattlerUnit>();

            switch (targetType)
            {
                case ETargetType.None:
                    break;
                case ETargetType.Self:
                    returnList.Add(instigator);
                    break;
                case ETargetType.Enemy:
                    var enemies = BattleManager.Instance.GetTeam(instigator.m_isPlayerTeam, true);
                    switch (targetRange)
                    {
                        case ETargetRange.None:
                            break; //Error
                        case ETargetRange.Front:
                            returnList.Add(enemies[0]);
                            break;
                        case ETargetRange.Back:
                            returnList.Add(enemies[enemies.Count - 1]);
                            break;
                        case ETargetRange.Random:
                            returnList.Add(enemies[Random.Range(0, enemies.Count)]);
                            break;
                        case ETargetRange.Count:
                            break; //Error
                    }
                    break;
                case ETargetType.Ally:
                    var allies = BattleManager.Instance.GetTeam(instigator.m_isPlayerTeam);
                    switch (targetRange)
                    {
                        case ETargetRange.None:
                            break; //Error
                        case ETargetRange.Front:
                            returnList.Add(allies[0]);
                            break;
                        case ETargetRange.Back:
                            returnList.Add(allies[allies.Count - 1]);
                            break;
                        case ETargetRange.Random:
                            returnList.Add(allies[Random.Range(0, allies.Count)]);
                            break;
                        case ETargetRange.Count:
                            break; //Error
                    }
                    break;
                case ETargetType.AllEnemies:
                    returnList.AddRange(BattleManager.Instance.GetTeam(instigator.m_isPlayerTeam, true));
                    break;
                case ETargetType.AllAllies:
                    returnList.AddRange(BattleManager.Instance.GetTeam(instigator.m_isPlayerTeam));
                    break;
                case ETargetType.Count:
                    break;
            }

            return returnList;
        }

        private static void SolveAbilityEffect(List<AutoBattlerUnit> targets, AbilityEffect effect)
        {
            foreach (var target in targets)
            {
                SolveAbilityEffect(target, effect);
            }
        }

        private static void SolveAbilityEffect(AutoBattlerUnit target, AbilityEffect effect)
        {
            switch (effect.AbilityEffectType)
            {
                case EAbilityEffect.Damage:
                    target.ReceiveDamage(effect.Magnitude);
                    break;
                case EAbilityEffect.Healing:
                    target.HealDamage(effect.Magnitude);
                    break;
                case EAbilityEffect.Count:
                    break;
            }
        }
    }
}
