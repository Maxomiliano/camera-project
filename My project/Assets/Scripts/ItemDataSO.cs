using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Tool,
    Weapon,
    Armor
}

public enum ActionType
{
    MeleeHit,
    RangeHit,
    Capture
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public Sprite icon;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public string itemName;
    public string itemAction;
    public float initialBatteryAmmount;
    public float currentBatteryAmmount;
    public bool stackable = true;

    /*
    public ItemData GetData()
    {
        ItemData itemData = new ItemData
        {
            icon = icon,
            itemName = itemName,
            itemAction = itemAction,
            prebaf = prebaf,
            type = type,
            actionType = actionType,
            stackable = stackable,
            initialBatteryAmmount = initialBatteryAmmount,
            currentBatteryAmmount = initialBatteryAmmount

        };
        return itemData;
    }
    */
    public ItemData GetData()
    {
        return new ItemData(this);
    }
}
public class ItemData
{
    public ItemDataSO itemDataSO;
    public Sprite icon;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public string itemName;
    public string itemAction;
    public float initialBatteryAmmount;
    public float currentBatteryAmmount;
    public bool stackable = true;

    public ItemData(ItemDataSO dataSO)
    {
        itemDataSO = dataSO;
        icon = dataSO.icon;
        itemName = dataSO.itemName;
        itemAction = dataSO.itemAction;
        prebaf = dataSO.prebaf;
        type = dataSO.type;
        actionType = dataSO.actionType;
        stackable = dataSO.stackable;
        initialBatteryAmmount = dataSO.initialBatteryAmmount;
        currentBatteryAmmount = dataSO.initialBatteryAmmount;
    }
}
