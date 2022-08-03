using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    protected List<UnitData> m_unitsPool = new List<UnitData>();
    protected List<UnitData> m_currentDraftedUnits = new List<UnitData>();
    [SerializeField]
    protected List<Image> m_unitsImages = new List<Image>();

    private void Start()
    {
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

            m_unitsImages[i].sprite = m_currentDraftedUnits[i].Sprite;
        }
    }
}
