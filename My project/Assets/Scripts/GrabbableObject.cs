using UnityEngine;

public class GrabbableObject : ToolbarItem, IInteractable
{
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] ItemDataSO itemDataSO;
    private ItemData _curentData;

    public ItemDataSO ItemData { get => itemDataSO; }
    public ItemData CurentData { get => _curentData; set => _curentData = value; }

    private void Awake()
    {
        _curentData = itemDataSO.GetData();
    }

    public void SetData(ItemData data)
    {
        _curentData = data;
    }

    public ItemData PickItem()
    {
        ItemData data = _curentData;
        Destroy(gameObject);
        return data;
    }

    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            ItemData data = PickItem();
            inventory.AddItem(data);
        }
    }
    
    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
