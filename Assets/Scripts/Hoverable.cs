using System;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public Action<Hoverable> OnBeginHover;
    public Action<Hoverable> OnEndHover;

    private void Start()
    {
        ShopManager.Instance?.RegisterHoverableObjects(this);
    }

    public void OnMouseEnter()
    {
        OnBeginHover?.Invoke(this);
    }

    public void OnMouseExit()
    {
        OnEndHover?.Invoke(this);
    }
}
