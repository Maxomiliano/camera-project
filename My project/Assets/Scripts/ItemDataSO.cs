using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemDataSO : ScriptableObject
{
    //public Sprite icon;
    public string itemName;
    public GameObject prebaf;
}
