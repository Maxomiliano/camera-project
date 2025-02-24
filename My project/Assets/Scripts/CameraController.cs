using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour, IRechargeable
{
    [SerializeField] Photographer photographer;
    [SerializeField] Inventory inventory;
    [SerializeField] Transform handTransform;
    [SerializeField] GameObject cameraPrefab;
    [SerializeField] GameObject playerFollowCamera;
    private bool m_hasCamera;
    private GameObject equippedCamera;
    private bool isAiming = false;

    private float currentBatteryPercentage;
    private float maxBatteryPercentage = 100;

    public float CurrentBatteryPercentage
    {
        get => currentBatteryPercentage;
    }

    public bool HasCamera
    {
        get => m_hasCamera;
        set
        {
            m_hasCamera = value;
            photographer.enabled = m_hasCamera;
        }
    }

    private void Start()
    {
        Photographer.OnScreenshotTaken += DecreaseBattery;
        currentBatteryPercentage = maxBatteryPercentage;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipCamera();
        }
        if (HasCamera && isAiming && Mouse.current.leftButton.wasPressedThisFrame)
        {
            photographer.TakeSnap();
        }

        if (HasCamera && Mouse.current.rightButton.isPressed)
        {
            AimCamera();
        }
        else
        {
            ReleaseCamera();
        }
    }

    private void AimCamera()
    {
        //Animacion de camara
        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 20;
        isAiming = true;
    }

    private void ReleaseCamera()
    {
        //Animacion de camara
        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 40;
        isAiming = false;
    }



    private void EquipCamera()
    {
        foreach (GrabbableObject item in inventory.GetAllItems())
        {
            SetHasCamera(true);
            InstantiateCamera();
            Debug.Log("Camara Equipada");
            return;
        }
        SetHasCamera(false);
        Debug.Log("No tienes una camara en el inventario");
    }

    private void InstantiateCamera()
    {
        if (equippedCamera != null) Destroy(equippedCamera);

        equippedCamera = Instantiate(cameraPrefab, handTransform);
        equippedCamera.transform.localPosition = Vector3.zero;
        equippedCamera.transform.localRotation = Quaternion.identity;
        equippedCamera.SetActive(true);
    }

    //Esta funcion se llamaria al presionar la tecla correspondiente de slot de inventario
    public void SetHasCamera(bool hasCam = true)
    {
        HasCamera = hasCam;
    }

    public void DecreaseBattery(float batteryAmmount)
    {
        if (currentBatteryPercentage > 0)
        {
            currentBatteryPercentage -= batteryAmmount;
            Debug.Log($"battery decreased by {batteryAmmount}");
        }
        else
        {
            Debug.Log("You have yo recharge the battery");
            return;
        }
    }

    public void RechargeBattery(float batteryAmmount)
    {
        if (currentBatteryPercentage == maxBatteryPercentage)
        {
            return;
        }
        currentBatteryPercentage += batteryAmmount;
    }
}
