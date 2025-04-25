using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : ToolbarItem, IRechargeable
{
    private ItemData _itemData;
    private Photographer _photographer;
    private GameObject _playerFollowCamera;
    private bool _isAiming;

    public Action OnBatteryValueChanged;
    public float CurrentBatteryPercentage => _itemData.CurrentBatteryAmmount;
    public float MaxBatteryPercentage => _itemData.maxBatteryAmmount;

    public void Initialize(ItemData itemData)
    {
        _itemData = itemData;
        _photographer = FindFirstObjectByType<Photographer>();
        _playerFollowCamera = GameObject.Find("PlayerFollowCamera");
    }
    public void RechargeBattery(float ammount)
    {
        _itemData.CurrentBatteryAmmount = Mathf.Min(_itemData.CurrentBatteryAmmount + ammount, _itemData.maxBatteryAmmount);
        OnBatteryValueChanged?.Invoke();
    }

    public void DecreaseBattery(float ammount)
    {
        _itemData.CurrentBatteryAmmount = Mathf.Max(_itemData.CurrentBatteryAmmount - ammount, 0f);
        if (_itemData.CurrentBatteryAmmount <= 0)
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
        if (_playerFollowCamera == null) return;
        //Animacion de camara
        _isAiming = false;
        _playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 40;
    }

    public override void OnPrimaryUse()
    {
        if (_isAiming && CurrentBatteryPercentage > 0)
        {
            DecreaseBattery(_photographer.batteryPerShot);
            _photographer.TakeSnap();
        }
    }

    public override void OnToolbarDeselected()
    {
        _photographer.enabled = false;
    }

    [ContextMenu("Recharge Battery Debugger")]
    public void RechargeDebugger()
    {
        RechargeBattery(10f);
    }
    [ContextMenu("Use camera debugger")]
    public void UseCamera()
    {
        DecreaseBattery(10f);
    }
}
