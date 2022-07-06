using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "Ability")]
    public class AbilityData : ScriptableObject
    {
        public float AbilityCooldown = 5.0f;
        public List<AbilityEffect> AbilityEffects = new List<AbilityEffect>();
    }

    [System.Serializable]
    public struct AbilityEffect
    {
        public ETargetType TargetType;
        public ETargetRange TargetRange;
        public EAbilityEffect AbilityEffectType;
        public int Magnitude;
    }
}