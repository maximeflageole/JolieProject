using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    protected Transform m_origin;
    protected RectTransform m_draggingPlane;
    public UnitData UnitData { get; protected set; }

    void Start()
    {
        m_origin = transform.parent.transform;
    }

    public void SetData(UnitData unitData)
    {
        UnitData = unitData;
        GetComponent<Image>().sprite = unitData.Sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_draggingPlane = transform as RectTransform;
    }

    public void OnDrag(PointerEventData data)
    {
        SetDraggedPosition(data);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ShopManager.Instance.OnShopItemReleased(this);

        Destroy(gameObject);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        var transform = data.pointerEnter.transform as RectTransform;

        var rt = GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_draggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
        }
    }
}
