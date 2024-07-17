using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance;
    public GameObject BackPack_UI;
    public Text CoinText;

    public Image[] ItemImages;
    private InventoryItemData[] inventoryItemDatas;

    private int defItemUsingCount = 0;
    private int speedItemUsingCount = 0;
    private int powerItemUsingCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventoryItemDatas = new InventoryItemData[ItemImages.Length];
    }

    void Update()
    {
        BackPackUIOn();
        CoinText.text = $"Coin : {GameManager.Instance.PlayerStat.Coin:N0}";
    }

    private void BackPackUIOn()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            BackPack_UI.SetActive(!BackPack_UI.activeSelf);
        }
    }

    internal bool AddItem(InventoryItemData item)
    {
        for (int i = 0; i < ItemImages.Length; i++)
        {
            if (ItemImages[i].sprite == null)
            {
                ItemImages[i].sprite = item.itemImage;
                inventoryItemDatas[i] = item;
                return true;
            }
        }
        return false;
    }

    public void ItemUse()
    {
        int siblingIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        InventoryItemData inventoryItem = inventoryItemDatas[siblingIndex];
        if (inventoryItem == null) return;

        if (inventoryItem.itemID == "HP")
        {
            GameManager.Instance.PlayerStat.HP += 10f;
            GameManager.Instance.PlayerStat.HP = Mathf.Min(GameManager.Instance.PlayerStat.HP, 100f);
            PopupMsgManager.Instance.ShowPopupMessage("체력이 10 회복 되었습니다.");
        } 
        else if (inventoryItem.itemID == "MP")
        {
            GameManager.Instance.PlayerStat.MP += 10f;
            GameManager.Instance.PlayerStat.MP = Mathf.Min(GameManager.Instance.PlayerStat.MP, 100f);
            PopupMsgManager.Instance.ShowPopupMessage("마나가 10 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "HP_Power")
        {
            GameManager.Instance.PlayerStat.HP = 100f;
            PopupMsgManager.Instance.ShowPopupMessage("체력 전체가 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "MP_Power")
        {
            GameManager.Instance.PlayerStat.MP = 100f;
            PopupMsgManager.Instance.ShowPopupMessage("마나 전체가 회복 되었습니다.");
        }
        else if (inventoryItem.itemID == "Def")
        {
            StartCoroutine(DefItem());
        }
        else if (inventoryItem.itemID == "Speed")
        {
            StartCoroutine(SpeedItem());
        }
        else if (inventoryItem.itemID == "Power")
        {
            StartCoroutine(PowerItem());
        }
        else if (inventoryItem.itemID == "Super")
        {
            //보스전 나올 때 구현
        }
        else
        {
            Debug.LogError($"존재하지 않는 itemID[{inventoryItem.itemID}]");
            return;
        }

        inventoryItemDatas[siblingIndex] = null;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;
    }

    IEnumerator DefItem()
    {
        defItemUsingCount++;
        GameManager.Instance.PlayerStat.Def *= 2;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log("1. PlayerDef : " + GameManager.Instance.PlayerStat.Def);
        yield return new WaitForSeconds(10f);

        defItemUsingCount--;
        GameManager.Instance.PlayerStat.Def /= 2;
        if (defItemUsingCount == 0)
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. PlayerDef : " + GameManager.Instance.PlayerStat.Def);
    }

    IEnumerator SpeedItem()
    {
        speedItemUsingCount++;
        GameManager.Instance.Character.Speed *= 2;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("1. PlayerSpeed : " + GameManager.Instance.Character.Speed);
        yield return new WaitForSeconds(10f);

        speedItemUsingCount--;
        GameManager.Instance.Character.Speed /= 2;
        if (speedItemUsingCount == 0)
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. PlayerSpeed : " + GameManager.Instance.Character.Speed);
    }

    IEnumerator PowerItem()
    {
        powerItemUsingCount++;
        GameManager.Instance.CharacterAttack.AttackDamage *= 2;
        GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("1. Player Power : " + GameManager.Instance.CharacterAttack.AttackDamage);
        yield return new WaitForSeconds(10f);

        powerItemUsingCount--;
        GameManager.Instance.CharacterAttack.AttackDamage /= 2;
        if (powerItemUsingCount == 0)
            GameManager.Instance.Character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. Player Power : " + GameManager.Instance.CharacterAttack.AttackDamage);
    }
}
