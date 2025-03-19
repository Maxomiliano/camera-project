using UnityEngine;

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
    public bool HasBattery;

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