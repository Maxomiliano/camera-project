using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 3;
    private List<EquippableObject> items = new List<EquippableObject>();
    private EquippableObject equipedItem;

    public EquippableObject EquipedItem { get => equipedItem; set => equipedItem = value; }

    public bool AddItem(EquippableObject item)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventario Lleno");
            return false;
        }
        items.Add(item);
        Debug.Log($"Objeto {item.name} agregado al inventario");
        return true;
    }

    public void RemoveItem(EquippableObject item) 
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Objeto {item.name} quitado del inventario");
        }
    }

    public EquippableObject GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public bool Contains(EquippableObject item)
    {
        return items.Contains(item);
    }

    public List<EquippableObject> GetAllItems()
    {
        return items;
    }

    public EquippableObject GetEquippedItem()
    {
        return EquipedItem;
    }

    public void EquipItem(EquippableObject item, Transform handPosition)
    {
        if (EquipedItem != null)
        {
            EquipedItem.OnUnequip();
        }
        if (item == null)
        {
            Debug.LogError("Intentaste equipar un objeto nulo.");
            return;
        }
        equipedItem = item;
        if (EquipedItem != null)
        {
            equipedItem.OnEquip(handPosition);
        }
        Debug.Log($"Objeto equipado: {equipedItem.name}");
    }

    //Drop equipped item
    public void UnequipItem()
    {
        if (EquipedItem != null)
        {
            Destroy(EquipedItem.gameObject);
            EquipedItem = null;
        }
    }

    /*
    public void ReEquipItem(GrabbableObject item)
    {
        
    }
    */
}
