using UnityEngine;

public class UnitSlot : Hoverable
{
    public UnitData UnitData { get; protected set; }
    [SerializeField]
    protected SpriteRenderer m_unitSR;

    public void AddUnit(ShopItem unit)
    {
        UnitData = unit.UnitData;
        m_unitSR.sprite = unit.UnitData.Sprite;
        m_unitSR.gameObject.SetActive(true);
    }
}