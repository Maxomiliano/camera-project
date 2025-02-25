using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : Rechargeable
{
    [SerializeField] Photographer photographer;
    [SerializeField] Inventory inventory;
    [SerializeField] Transform handTransform;
    [SerializeField] GameObject cameraPrefab;
    [SerializeField] GameObject playerFollowCamera;
    private bool m_hasCamera;
    private GameObject equippedCamera;
    private bool isAiming = false;

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
        Debug.Log("Intentando equipar la c�mara...");
        foreach (var item in inventory.GetAllItems())
        {
            if (item.name == "CameraObject")
            {
                inventory.EquipItem(item);
                Debug.Log($"Camara equipada en inventario: {item.name}");
                break;
            }
        }

        GrabbableObject cameraItem = inventory.GetEquippedItem();
        if (cameraItem != null)
        {
            Debug.Log($"Objeto equipado: {cameraItem.name}");
            SetHasCamera(true);
            InstantiateCamera();
            Debug.Log("Camara Equipada");
        }
        else
        {
            SetHasCamera(false);
            Debug.Log("No tienes una camara en el inventario");
        }
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
}
