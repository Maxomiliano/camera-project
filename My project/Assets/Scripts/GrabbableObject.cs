using UnityEngine;

public class GrabbableObject : ToolbarItem, IInteractable
{
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] ItemDataSO itemData;

    private ItemInstance _itemInstance;
    public ItemInstance ItemInstance { get => _itemInstance; }
    public ItemDataSO ItemData { get => itemData; }

    private void Start()
    {
        if (itemData != null)
        {
            _itemInstance = new ItemInstance(itemData);
        }
    }

    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null)
        {
            _itemInstance = new ItemInstance(itemData);
            if (inventory.AddItem(_itemInstance))
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetItemInstance(ItemInstance newItemInstance)
    {
        _itemInstance = newItemInstance;
    }

    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
