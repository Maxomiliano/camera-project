using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : EquippableObject
{
    [SerializeField] GameObject cameraPrefab;
    [SerializeField] Rechargeable rechargeable;

    private Photographer photographer;
    private GameObject playerFollowCamera;

    private GameObject equippedCamera;
    private bool m_isAiming;

    public Rechargeable Rechargeable { get => rechargeable; }

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        Photographer.OnScreenshotTaken += rechargeable.DecreaseBattery;
    }

    private void OnDestroy()
    {
        Photographer.OnScreenshotTaken -= rechargeable.DecreaseBattery;
    }

    private void Initialize()
    {
        photographer = FindFirstObjectByType<Photographer>();
        playerFollowCamera = GameObject.Find("PlayerFollowCamera");
    }

    public override void PrepareObject()
    {
        //Animacion de camara
        m_isAiming = true;
        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 20;
    }

    public override void UnprepareObject()
    {
        //Animacion de camara
        m_isAiming = false;
        playerFollowCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = 40;
    }

    public override void UseObject()
    {
        if (m_isAiming && Rechargeable.CurrentBatteryPercentage > 0)
        { 
            photographer.TakeSnap();
        }
    }

    public override void OnEquip(Transform handPosition)
    {
        if (equippedCamera != null) Destroy(equippedCamera);

        equippedCamera = Instantiate(cameraPrefab, handPosition);
        equippedCamera.transform.localPosition = Vector3.zero;
        equippedCamera.transform.localRotation = Quaternion.identity;
        equippedCamera.SetActive(true);
        photographer.enabled = true;
    }
    public override void OnUnequip()
    {
        photographer.enabled = false;
    }
}
