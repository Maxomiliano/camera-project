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
    public float maxBatteryAmmount;
    public float currentBatteryAmmount;
    public bool stackable = true;

    
    public ItemData GetData()
    {
        ItemData itemData = new ItemData
        {
            itemName = itemName,
            icon = icon,
            itemAction = itemAction,
            prebaf = prebaf,
            type = type,
            actionType = actionType,
            stackable = stackable,
            currentBatteryAmmount = maxBatteryAmmount,
            maxBatteryAmmount = maxBatteryAmmount,

        };
        return itemData;
    }
}
public class ItemData
{
    public Sprite icon;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public string itemName;
    public string itemAction;
    public float maxBatteryAmmount;
    public float currentBatteryAmmount;
    public bool stackable = true;
    private float v;
}
