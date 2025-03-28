using TMPro;
using UnityEngine;

//Este funcionaría como el WorldItem
public class GrabbableObject : ToolbarItem, IInteractable
{
    [SerializeField] private ObjectIdentifier objectIdentifier;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private ItemDataSO _itemDataSO;

    private ItemData _currentData;
    private IRechargeable _rechargeable;
    public ItemDataSO ItemData { get => _itemDataSO; }
    public ItemData CurrentData { get => _currentData; set => _currentData = value; }
    CameraController cameraController;

    private void Start()
    {
        cameraController = GetComponent<CameraController>();
        cameraController.OnBatteryValueChanged += Refresh;
    }

    private void Awake()
    {
        _rechargeable = GetComponent<IRechargeable>();
        _currentData = ItemData.GetData();
    }

    [ContextMenu("Set Data Debugger")]
    public void SetDataDebugger()
    {
        SetData(_itemDataSO.GetData());
    }

    public void SetData(ItemData data)
    {
        _currentData = data;
        Refresh();
    }

    public ItemData PickItem()
    {
        //Esta funcion me devuelve la data que tenia el objeto luego de destruirlo
        ItemData data = _currentData;
        Destroy(this.gameObject, 0.5f);
        return data;
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        if (_currentData == null)
        {
            return;
        }
        _nameText.text = _currentData.itemName;

        if (_rechargeable != null)
        {
            _currentData.currentBatteryAmmount = _rechargeable.CurrentBatteryPercentage;            
        }
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
