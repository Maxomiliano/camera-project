using UnityEngine;

public class DemoScripts : MonoBehaviour
{
    /*
    public Inventory inventory;
    public ItemDataSO[] itemToPickup;

    public void PickupItem(int id)
    {
       bool result = inventory.AddItem(itemToPickup[id]);
        if (result)
        {
            Debug.Log("_item added");
        }
        else 
        {
            Debug.Log("_item NOT added");
        }
    }

    public void GetSelectedItem()
    {
        ItemDataSO receivedItem = inventory.GetSelectedItem(false);
        if (receivedItem != null)
        {
            Debug.Log("Received item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item received!!");
        }
    }

    public void UseSelectedItem()
    {
        ItemDataSO receivedItem = inventory.GetSelectedItem(true);
        if (receivedItem != null)
        {
            Debug.Log("Used item: " + receivedItem);
        }
        else
        {
            Debug.Log("No item used!!");
        }
    }
    */

    [SerializeField] private GrabbableObject grabbableObject;
    public GrabbableObject _currentInstantiatedItem;

    [ContextMenu("Instantiate item A")]
    public void InstantiateItem()
    {
        _currentInstantiatedItem = Instantiate(grabbableObject);
        _currentInstantiatedItem.SetData(_currentInstantiatedItem.CurrentData);
    }

    [ContextMenu("Use Camera")]
    public void UseCamera()
    {
        CameraController cameraController = FindFirstObjectByType<CameraController>();
        cameraController.DecreaseBattery(10f);
    }

    [ContextMenu("Pick current spawned item")]
    public void PickCurrentSpawnedItem()
    {
        Inventory.Instance.AddItem(_currentInstantiatedItem.PickItem());
    }

    [ContextMenu("Pop from inventory")]
    public void PopFromInventory()
    {
        ItemData data = Inventory.Instance.PopItem();
        _currentInstantiatedItem = Instantiate(grabbableObject);
        _currentInstantiatedItem.SetData(data);
    }
}
