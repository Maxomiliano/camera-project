using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public InventorySlot[] inventorySlots;
    public List<ItemData> _items = new List<ItemData>();
    public List<InventoryItem> _inventoryItems = new List<InventoryItem>();

    public GameObject inventoryItemPrefab;
    public Transform handPosition;
    public int maxStackItems = 10;

    private int selectedSlot = 0;

    private ToolbarItem toolbarItem;
    public ToolbarItem ToolbarItem { get => toolbarItem; set => toolbarItem = value; }
    public int SelectedSlot { get => selectedSlot; set => selectedSlot = value; }

    private void Awake()
    {
        Instance = this;
        Refresh();
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
            ShowItemInHand(inventoryItem.CurrentData, handPosition);
        }
        else
        {
            ShowItemInHand(null, handPosition);
        }
    }

    public void AddItem(ItemData item)
    {
        _items.Add(item);
        Refresh();
    }

    public void Refresh()
    {
        foreach (InventoryItem image in _inventoryItems)
        {
            //Destruir objeto
            image.gameObject.SetActive(false);
        }

        for (int i = 0; i < _items.Count; i++)
        {
            InventoryItem image = _inventoryItems[i];
            ItemData item = _items[i];
            //Crear objeto
            if (image != null && item != null)
            {
                image.gameObject.SetActive(true);
                image.Initialize(item);
            }
        }
    }

    public void PopItem(ItemData itemToDrop)
    {
        if (itemToDrop == null || !_items.Contains(itemToDrop))
        {
            Debug.LogError("Item no encontrado en el inventario.");
            return;
        }
        int index = _items.IndexOf(itemToDrop);
        if (index >= 0)
        {
            _items[index] = null;
        }
        //_items.Remove(itemToDrop);
        Refresh();
        DeselectItem();

        if (itemToDrop.prebaf != null)
        {
            GameObject droppedItem = Instantiate(itemToDrop.prebaf);
            //droppedItem.transform.position = GetDropPosition();

            GrabbableObject grabbableObject = droppedItem.GetComponent<GrabbableObject>();
            if (grabbableObject != null)
            {
                grabbableObject.SetData(itemToDrop);
            }
            else
            {
                Debug.LogError("El objeto soltado no tiene un componente GrabbableObject.");
            }
        }
    }

    private Vector3 GetDropPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
    }

    //Si USE es verdadero entonces deberiamos deshacernos de este objeto o desincrementar su count
    //El bool USE seria para spawnear items por fuera del inventario. Ver significado.
    public ItemData GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            ItemData item = itemInSlot.CurrentData;
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

    public void ShowItemInHand(ItemData item, Transform handPosition)
    {
        if (item == null)
        {
            return;
        }
        if (item.prebaf == null)
        {
            DeselectItem();
            Debug.LogError("Intentaste equipar un objeto sin prefab.");
            return;
        }

        GameObject obj = Instantiate(item.prebaf, handPosition);
        Debug.Log($"Objeto instanciado: {obj.name}, Parent: {obj.transform.parent?.name}");
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        toolbarItem = obj.GetComponent<ToolbarItem>();
        GrabbableObject grabbableItem = toolbarItem.GetComponent<GrabbableObject>();
        if (grabbableItem != null)
        {
            grabbableItem.SetData(item); // Restaurar los datos dinámicos.
        }

        if (ToolbarItem != null)
        {
            toolbarItem.OnToolbarSelected(handPosition);
        }
        Debug.Log($"Objeto equipado: {toolbarItem.name}");
    }

    public bool RemoveItem(ItemData item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];  //Variable del slot
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //Variable del item que estaría en el slot
            if (itemInSlot != null && itemInSlot.CurrentData == item && itemInSlot._count > 0) //Acá checkeo que el slot tenga un item y que tenga el mismo item que estoy agarrando
            {
                itemInSlot._count--;
                itemInSlot.RefreshCount(); //Para aumentar el numero de stack en la UI
                return true;
            }
        }
        return false;
    }

    public ToolbarItem GetSelectedItem()
    {
        return ToolbarItem;
    }

    public ItemData DeselectItem()
    {
        if (ToolbarItem != null)
        {
            ItemData itemData = ToolbarItem.GetComponent<GrabbableObject>()?.CurrentData;
            if (itemData != null)
            {
                IRechargeable rechargeable = ToolbarItem.GetComponent<IRechargeable>();
                if (rechargeable != null)
                {
                    itemData.currentBatteryAmmount = rechargeable.CurrentBatteryPercentage;
                }
            }

            //Destroy(ToolbarItem.gameObject);
            ToolbarItem = null;
            Debug.Log("Objeto en la mano eliminado correctamente.");

            return itemData;
        }
        else
        {
            Debug.Log("No había objeto equipado para eliminar.");
            return null;
        }
    }
}
