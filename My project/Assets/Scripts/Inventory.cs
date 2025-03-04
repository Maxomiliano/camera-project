using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 3;
    private List<GrabbableObject> items = new List<GrabbableObject>();
    private GrabbableObject equipedItem;

    public GrabbableObject EquipedItem { get => equipedItem; set => equipedItem = value; }

    public bool AddItem(GrabbableObject item)
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

    public void RemoveItem(GrabbableObject item) 
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Objeto {item.name} quitado del inventario");
        }
    }

    public GrabbableObject GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public bool Contains(GrabbableObject item)
    {
        return items.Contains(item);
    }

    public List<GrabbableObject> GetAllItems()
    {
        return items;
    }

    public GrabbableObject GetEquippedItem()
    {
        return EquipedItem;
    }

    public void EquipItem(GrabbableObject item, Transform handPosition)
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
