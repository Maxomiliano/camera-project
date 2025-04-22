using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : ToolbarItem, IRechargeable
{
    //[SerializeField] private float _maxBatteryPercentage = 100f;
    //[SerializeField] private float _currentBatteryPercentage;

    private ItemData _itemData;
    private Photographer _photographer;
    private GameObject _playerFollowCamera;
    private bool _isAiming;

    public Action OnBatteryValueChanged;
    public float CurrentBatteryPercentage => _itemData.currentBatteryAmmount;
    public float MaxBatteryPercentage => _itemData.maxBatteryAmmount;


    private void Awake()
    {
        //Initialize(_itemData);
    }

    private void Start()
    {
        Photographer.OnScreenshotTaken += DecreaseBattery;   
    }

    private void OnDestroy()
    {
        Photographer.OnScreenshotTaken -= DecreaseBattery;
    }

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        //_itemData.currentBatteryAmmount = _itemData.maxBatteryAmmount;
        _photographer = FindFirstObjectByType<Photographer>();
        _playerFollowCamera = GameObject.Find("PlayerFollowCamera");
    }

    [ContextMenu("Recharge Battery Debugger")]
    public void RechargeDebugger()
    {
        RechargeBattery(10f);
    }
    public void RechargeBattery(float ammount)
    {
        _itemData.currentBatteryAmmount = Mathf.Min(_itemData.currentBatteryAmmount + ammount, _itemData.maxBatteryAmmount);
        OnBatteryValueChanged?.Invoke();
    }

    [ContextMenu("Use camera debugger")]
    public void UseCamera()
    {
        DecreaseBattery(10f);
    }

    public void DecreaseBattery(float ammount)
    {
        _itemData.currentBatteryAmmount = Mathf.Max(_itemData.currentBatteryAmmount - ammount, 0f);
        if (_itemData.currentBatteryAmmount <= 0)
        {
            Debug.Log("You have to recharge the battery");
        }
        else
        {
            Debug.Log($"Battery decreased by {ammount}");
        }
        OnBatteryValueChanged?.Invoke();
    }

    public override void OnSecondaryUse()
    {
        //Animacion de camara
        _isAiming = true;
        _playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 20;
    }

    public override void OnSecondaryRelease()
    {
        //Animacion de camara
        _isAiming = false;
        _playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 40;
    }

    public override void OnPrimaryUse()
    {
        if (_isAiming && CurrentBatteryPercentage > 0)
        { 
            _photographer.TakeSnap();
        }
        //Aca hay que llamar a Refresh() suponiendo que queremos ver esos datos en la UI.
    }

    public override void OnToolbarDeselected()
    {
        _photographer.enabled = false;
    }
}
