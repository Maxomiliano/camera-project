using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 3;
    private List<GrabbableObject> items = new List<GrabbableObject>();

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
}
