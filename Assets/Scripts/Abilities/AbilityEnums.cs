namespace AbilitySystem
{
    public enum ETargetType
    {
        None,
        Self,
        Enemy,
        Ally,
        AllEnemies,
        AllAllies,
        Count
    }

    public enum ETargetRange
    {
        None,
        Front,
        Back,
        Random,
        Count
    }

    public enum EAbilityEffect
    {
        Damage,
        Healing,
        Count
    }
}
