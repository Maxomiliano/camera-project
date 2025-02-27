using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 3;
    private List<ItemDataSO> items = new List<ItemDataSO>();
    private EquippableObject equipedItem;

    public EquippableObject EquipedItem { get => equipedItem; set => equipedItem = value; }

    public bool AddItem(ItemDataSO item)
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

    public void RemoveItem(ItemDataSO item) 
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"Objeto {item.name} quitado del inventario");
        }
    }

    public ItemDataSO GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
    }

    public bool Contains(ItemDataSO item)
    {
        return items.Contains(item);
    }

    public List<ItemDataSO> GetAllItems()
    {
        return items;
    }

    public EquippableObject GetEquippedItem()
    {
        return EquipedItem;
    }

    public void EquipItem(ItemDataSO item, Transform handPosition)
    {
        if (EquipedItem != null)
        {
            EquipedItem.OnUnequip();
            Destroy(EquipedItem.gameObject);
        }
        if (item == null || item.prebaf == null)
        {
            Debug.LogError("Intentaste equipar un objeto nulo o sin prefab.");
            return;
        }
        GameObject obj = Instantiate(item.prebaf, handPosition);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        equipedItem = obj.GetComponent<EquippableObject>();

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
}
