using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int maxStackItems = 10;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform handPosition;

    private List<ItemDataSO> items = new List<ItemDataSO>();
    private int selectedSlot = 0;

    private EquippableObject equipedItem;
    public EquippableObject EquipedItem { get => equipedItem; set => equipedItem = value; }
    public int SelectedSlot { get => selectedSlot; set => selectedSlot = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeSelectedSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeSelectedSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeSelectedSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeSelectedSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeSelectedSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeSelectedSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7)) ChangeSelectedSlot(6);
    }

    private void ChangeSelectedSlot(int newValue) //La variable newvalue diria qué slot es el nuevo
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;

        InventoryItem inventoryItem = inventorySlots[newValue].GetComponentInChildren<InventoryItem>();
        if (inventoryItem != null)
        {
            ShowItemInHand(inventoryItem._item, handPosition);
        }
        else
        {
            ShowItemInHand(null, handPosition);
        }
    }

    public bool AddItem(ItemDataSO item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];  //Variable del slot
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //Variable del item que estaría en el slot
            if (itemInSlot != null && itemInSlot._item == item && itemInSlot._count < maxStackItems && itemInSlot._item.stackable) //Acá checkeo que el slot tenga un item y que tenga el mismo item que estoy agarrando
            {
                itemInSlot._count++;
                itemInSlot.RefreshCount(); //Para aumentar el numero de stack en la UI
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];  //Variable del slot
            InventoryItem itemSlot = slot.GetComponentInChildren<InventoryItem>(); //Variable del item que estaría en el slot
            if (itemSlot == null) //Acá checkeo que el slot esté vacío, es decir que no tiene un item
            {
                SpawnNewItem(item, slot);
                if (inventorySlots[selectedSlot].GetComponentInChildren<InventoryItem>() == null)
                { 
                    ChangeSelectedSlot(i);
                }
                if (selectedSlot == i)
                {
                    ShowItemInHand(item, handPosition);
                }
                return true;
            }
        }
        return false;
    }

    private void SpawnNewItem(ItemDataSO item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.Initialize(item);
    }

    //Si USE es verdadero entonces deberiamos deshacernos de este objeto o desincrementar su count
    //El bool USE seria para spawnear items por fuera del inventario. Ver significado.
    public ItemDataSO GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            ItemDataSO item = itemInSlot._item;
            if (use == true)
            {
                itemInSlot._count--;
                if (itemInSlot._count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }

    public void ShowItemInHand(ItemDataSO item, Transform handPosition)
    {
        UnequipItem();

        if (item == null)
        {
            return;
        }
        if (item.prebaf == null)
        {
            UnequipItem();
            Debug.LogError("Intentaste equipar un objeto sin prefab.");
            return;
        }

        GameObject obj = Instantiate(item.prebaf, handPosition);
        Debug.Log($"Objeto instanciado: {obj.name}, Parent: {obj.transform.parent?.name}");
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        equipedItem = obj.GetComponent<EquippableObject>();

        if (EquipedItem != null)
        {
            equipedItem.OnEquip(handPosition);
        }
        Debug.Log($"Objeto equipado: {equipedItem.name}");
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

    public EquippableObject GetEquippedItem()
    {
        return EquipedItem;
    }

    public void UnequipItem()
    {
        if (EquipedItem != null)
        {
            Destroy(EquipedItem.gameObject);
            EquipedItem = null;
            Debug.Log("Objeto en la mano eliminado correctamente.");
        }
        else
        {
            Debug.Log("No había objeto equipado para eliminar.");
        }
    }
}
