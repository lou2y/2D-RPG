using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public InventoryItemData[] items;
    public GameObject Purchase_UI;
    public Image ItemImage;
    public Text ItemNameText;
    public Text ItemCoinText;
    public Text ItemExplainText;

    private Dictionary<string, InventoryItemData> itemDictionary;
    private string selectedItemID;

    void Start()
    {
        itemDictionary = new Dictionary<string, InventoryItemData>();
        foreach (InventoryItemData item in items)
        {
            itemDictionary[item.itemID] = item;
        }
    }

    public void SelectItem(string itemID)
    {
        if (itemDictionary.TryGetValue(itemID, out InventoryItemData selectedItem))
        {
            Purchase_UI.SetActive(true);
            ItemImage.sprite = selectedItem.itemImage;
            ItemNameText.text = selectedItem.itemName;
            ItemCoinText.text = $"{selectedItem.itemPrice:N0} Point";
            ItemExplainText.text = selectedItem.itemDescription;

            selectedItemID = itemID;
        }
        else
        {
            Debug.LogError("Item ID not found : " + itemID);
        }
    }

    public void Purchase()
    {
        InventoryItemData selectedItem = itemDictionary[selectedItemID];
        if (GameManager.Instance.PlayerStat.Coin >= selectedItem.itemPrice)
        {
            if (BackPackManager.Instance.AddItem(selectedItem))
            {
                GameManager.Instance.PlayerStat.Coin -= selectedItem.itemPrice;
                PopupMsgManager.Instance.ShowPopupMessage("구매 성공");
            }
            else
            {
                PopupMsgManager.Instance.ShowPopupMessage("BackPack에 빈 공간이 없습니다.");
            }
        }
        else
        {
            PopupMsgManager.Instance.ShowPopupMessage($"잔액이 부족합니다. 잔액: {GameManager.Instance.PlayerStat.Coin}");
        }
    }
}
