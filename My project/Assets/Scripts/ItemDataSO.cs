using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public Sprite icon;
    public string itemName;
    public string itemAction;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public bool stackable = true;
    public float initialBatteryAmmount;

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

public class ItemData
{
    public Sprite icon;
    public string itemName;
    public string itemAction;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public bool stackable = true;
    public float initialBatteryAmmount;
    public float currentBatteryAmmount;
}