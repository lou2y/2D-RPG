using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Image[] itemImages;
    public Text[] itemTexts;
    public InventoryItemData[] itemDatas;

    void Start()
    {
        for (int i = 0; i < itemDatas.Length; i++)
        {
            itemImages[i].sprite = itemDatas[i].itemImage;
            itemTexts[i].text = $"{itemDatas[i].itemName} ({itemDatas[i].itemPrice:N0}P)";
        }
    }
}
