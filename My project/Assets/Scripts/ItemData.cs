using System;
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
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public GameObject prebaf;
    public ItemType type;
    public ActionType actionType;
    public string itemName;
    public string itemAction;
    [SerializeField] private float currentBatteryAmmount;
    public float maxBatteryAmmount;
    public bool stackable = true;

    public float CurrentBatteryAmmount
    {
        get => currentBatteryAmmount;
        set
        {
            currentBatteryAmmount = value;
            OnBatteryValueChanged?.Invoke();
        }
    }
    public Action OnBatteryValueChanged;
}
