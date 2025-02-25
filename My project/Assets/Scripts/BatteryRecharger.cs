using System.Collections;
using UnityEngine;

public class BatteryRecharger : MonoBehaviour, IInteractable
{
    [SerializeField] float timeToRecharge = 10f;
    Inventory inventory;
    private Coroutine rechargeBatteryCoroutine;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void Interact()
    {
        GrabbableObject objectToRecharg = inventory.GetEquippedItem();
        if (objectToRecharg == null) return;

        CameraController cameraController = FindFirstObjectByType<CameraController>();
        if (cameraController == null) return;

        if (rechargeBatteryCoroutine != null)
        {
            StopCoroutine(rechargeBatteryCoroutine);
        }
        rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(cameraController));
    }

    private IEnumerator RechargeBattery(Rechargeable rechargableObj)
    {
        float rechargeRate = rechargableObj.MaxBatteryPercentage / timeToRecharge;
        while (rechargableObj.CurrentBatteryPercentage < rechargableObj.MaxBatteryPercentage)
        {
            rechargableObj.RechargeBattery(rechargeRate * Time.deltaTime);
            yield return null;
        }
        rechargableObj.RechargeBattery(rechargableObj.MaxBatteryPercentage);
        rechargeBatteryCoroutine = null;
    }
}
