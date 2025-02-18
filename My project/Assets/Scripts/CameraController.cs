using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Photographer photographer;
    [SerializeField] Inventory inventory;
    [SerializeField] Transform handTransform;
    [SerializeField] GameObject cameraPrefab;
    private bool m_hasCamera;
    private float batteryPercentage;
    private GameObject equippedCamera;

    public bool HasCamera
    {
        get => m_hasCamera;
        set
        {
            m_hasCamera = value;
            photographer.enabled = m_hasCamera;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipCamera();
        }
        if (HasCamera && Mouse.current.leftButton.wasPressedThisFrame)
        {
            photographer.TakeSnap();
        }
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
        if(equippedCamera != null) Destroy(equippedCamera);

        equippedCamera = Instantiate(cameraPrefab, handTransform);
        equippedCamera.transform.localPosition = Vector3.zero;
        equippedCamera.transform.localRotation = Quaternion.identity;
    }

    //Esta funcion se llamaria al presionar la tecla correspondiente de slot de inventario
    public void SetHasCamera(bool hasCam = true)
    {
        HasCamera = hasCam;
    }
}
