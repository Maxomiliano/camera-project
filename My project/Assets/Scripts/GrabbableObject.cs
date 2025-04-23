using TMPro;
using UnityEngine;

//Este funcionaría como el WorldItem
public class GrabbableObject : ToolbarItem, IInteractable
{
    [SerializeField] private ObjectIdentifier objectIdentifier;
    [SerializeField] private ItemDataSO _itemDataSO;

    private ItemData _currentData;
    private IRechargeable _rechargeable;
    public ItemDataSO ItemData { get => _itemDataSO; }
    public ItemData CurrentData { get => _currentData; set => _currentData = value; }
    CameraController cameraController;


    private void Awake()
    {
        if (_currentData == null)
        { 
            _currentData = ItemData.GetData();
        }
        Debug.Log($"Awake: CurrentData = {_currentData?.currentBatteryAmmount}");
    }
    private void Start()
    {
        _rechargeable = GetComponent<IRechargeable>();
        cameraController = GetComponent<CameraController>();
        cameraController.Initialize(_currentData);
        cameraController.OnBatteryValueChanged += Refresh;
    }

    private void OnDestroy()
    {
        cameraController.OnBatteryValueChanged -= Refresh;
    }

    public void SetData(ItemData data)
    {
        _currentData = data;
        Debug.Log($"SetData: Data passed = {data.currentBatteryAmmount}");
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
        if (_rechargeable != null)
        {
            _currentData.currentBatteryAmmount = _rechargeable.CurrentBatteryPercentage;
        }
        
        Debug.Log($"Refresh: CurrentData = {_currentData?.currentBatteryAmmount}");
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
