using UnityEngine;

public class DemoScripts : MonoBehaviour
{
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
