using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : ToolbarItem, IRechargeable
{
    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private float _maxBatteryPercentage = 100f;
    [SerializeField] private float _currentBatteryPercentage;

    private Photographer _photographer;
    private GameObject _playerFollowCamera;
    private bool _isAiming;

    public float CurrentBatteryPercentage => _currentBatteryPercentage;
    public float MaxBatteryPercentage => _maxBatteryPercentage;


    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        Photographer.OnScreenshotTaken += DecreaseBattery;
    }

    private void OnDestroy()
    {
        Photographer.OnScreenshotTaken -= DecreaseBattery;
    }

    private void Initialize()
    {
        _photographer = FindFirstObjectByType<Photographer>();
        _playerFollowCamera = GameObject.Find("PlayerFollowCamera");
    }

    public void RechargeBattery(float ammount)
    {
        _currentBatteryPercentage = Mathf.Min(_currentBatteryPercentage + ammount, _maxBatteryPercentage);
    }

    public void DecreaseBattery(float ammount)
    {
        _currentBatteryPercentage = Mathf.Max(_currentBatteryPercentage - ammount, 0f);
        if (_currentBatteryPercentage <= 0)
        {
            Debug.Log("You have to recharge the battery");
        }
        else
        {
            Debug.Log($"Battery decreased by {ammount}");
        }
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
    }

    public override void OnToolbarDeselected()
    {
        _photographer.enabled = false;
    }
}
