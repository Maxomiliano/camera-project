using TMPro;
using UnityEngine;

//Este funcionaría como el WorldItem
public class GrabbableObject : ToolbarItem, IInteractable
{
    [SerializeField] private ObjectIdentifier objectIdentifier;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private ItemDataSO _itemDataSO;

    private ItemData _currentData;
    public ItemDataSO ItemData { get => _itemDataSO; }
    public ItemData CurrentData { get => _currentData; set => _currentData = value; }

    private void Awake()
    {
        _currentData = ItemData.GetData();
    }

    public void SetData(ItemData data)
    {
        _currentData = data;
        Refresh();
    }

    public ItemData PickItem()
    {
        ItemData data = _currentData;
        Destroy(this.gameObject, 0.5f);
        return data;
    }

    public void Refresh()
    {
        if (_currentData == null)
        {
            return;
        }
        _nameText.text = _currentData.itemName;
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
