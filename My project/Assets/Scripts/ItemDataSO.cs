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
}
public class ItemData
{
    //public ItemDataSO itemDataSO;
    public Sprite icon;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public string itemName;
    public string itemAction;
    public float initialBatteryAmmount;
    public float currentBatteryAmmount;
    public bool stackable = true;
}
