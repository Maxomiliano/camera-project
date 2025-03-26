using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int maxStackItems = 10;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform handPosition;

    private List<ItemData> items = new List<ItemData>();
    private int selectedSlot = 0;

    private ToolbarItem toolbarItem;
    public ToolbarItem ToolbarItem { get => toolbarItem; set => toolbarItem = value; }
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
            ShowItemInHand(inventoryItem.CurrentData, handPosition);
        }
        else
        {
            ShowItemInHand(null, handPosition);
        }
    }

    //Este add item es un Refresh tambien.
    //Posiblemente esta funcion deba ser refactorizada para que por un lado haga items.Add()
    //y luego un Refresh()
    public bool AddItem(ItemData item)
    {
        /*
         //Esta seria la parte para actualizar el numero del stack en la UI
         for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];  //Variable del slot
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>(); //Variable del item que estaría en el slot
            if (itemInSlot != null && itemInSlot._item.Equals(item) && itemInSlot._count < maxStackItems && itemInSlot._item.stackable) //Acá checkeo que el slot tenga un item y que tenga el mismo item que estoy agarrando
            {
                itemInSlot._count++;
                itemInSlot.RefreshCount(); //Para aumentar el numero de stack en la UI
                return true;
            }
        }
        */


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

    private void SpawnNewItem(ItemData item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.Initialize(item);
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
        ItemData currentItemData = DeselectItem();
        //DeselectItem();

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
        if (grabbableItem != null && currentItemData != null)
        {
            grabbableItem.SetData(currentItemData); // Restaurar los datos dinámicos.
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


    public ItemData GetItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            return items[index];
        }
        return null;
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

            Destroy(ToolbarItem.gameObject);
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
