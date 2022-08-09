using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    protected List<UnitData> m_unitsPool = new List<UnitData>();
    protected List<UnitData> m_currentDraftedUnits = new List<UnitData>();
    [SerializeField]
    protected List<ShopItem> m_units = new List<ShopItem>();
    [SerializeField]
    public Hoverable m_hoveredObject = null;

    public static ShopManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (Instance == this)
            RefreshShopUnits();
    }

    public void RefreshShopUnits()
    {
        m_currentDraftedUnits.Clear();

        for (var i = 0; i < 6; i++)
        {
            var rand = Random.Range(0, m_unitsPool.Count);
            Debug.Log("Random number is " + rand);
            m_currentDraftedUnits.Add(m_unitsPool[rand]);

            m_units[i].SetData(m_currentDraftedUnits[i]);
        }
    }

    public void RegisterHoverableObjects(Hoverable hoverable)
    {
        hoverable.OnBeginHover += OnObjectHovered;
        hoverable.OnEndHover += OnObjectHoveredStop;
    }

    private void OnObjectHovered(Hoverable hoverable)
    {
        m_hoveredObject = hoverable;
        Debug.Log("Hovered an object");
    }

    private void OnObjectHoveredStop(Hoverable hoverable)
    {
        if (m_hoveredObject == hoverable)
        {
            m_hoveredObject = null;
        }
        Debug.Log("Stopped hovering an object");
    }

    public bool OnShopItemReleased(ShopItem shopItem)
    {
        if (m_hoveredObject != null)
        {
            var UnitSlot = m_hoveredObject as UnitSlot;
            UnitSlot.AddUnit(shopItem);
        }
        return m_hoveredObject != null;
    }
}
