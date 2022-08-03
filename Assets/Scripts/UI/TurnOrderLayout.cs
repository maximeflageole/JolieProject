using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderLayout : MonoBehaviour
{
    public List<Image> TurnOrderImages { get; private set; }

    void Start()
    {
        TurnOrderImages = new List<Image>();

        foreach(var image in GetComponentsInChildren<Image>())
        {
            TurnOrderImages.Add(image);
        }
    }

    public void PopulateImages(List<Sprite> unitsInOrder)
    {
        var i = 0;
        while (unitsInOrder.Count > i && TurnOrderImages.Count > i)
        {
            TurnOrderImages[i].sprite = unitsInOrder[i];
            i++;
        }
    }
}
