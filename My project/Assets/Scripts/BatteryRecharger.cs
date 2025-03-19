using System.Collections;
using UnityEngine;

public class BatteryRecharger : MonoBehaviour, IInteractable
{
    [SerializeField] float timeToRecharge = 10f;
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] Transform rechargPlace;
    Inventory inventory;
    private Coroutine rechargeBatteryCoroutine;
    private Rechargeable rechargingObject;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void Interact()
    {
        if (rechargingObject == null)
        {
            ToolbarItem objectToRecharg = inventory.GetSelectedItem();
            if (objectToRecharg == null) return;

            GrabbableObject grabbableObj = objectToRecharg.gameObject.GetComponent<GrabbableObject>();
            if (grabbableObj == null) return;

            ItemInstance itemInstance = grabbableObj.ItemInstance;
            if (itemInstance == null) return;

            GameObject oldObject = objectToRecharg.gameObject;

            inventory.RemoveItem(itemInstance);
            inventory.DeselectItem(false);

            GameObject rechargeableInstance = oldObject;
            rechargeableInstance.transform.SetParent(rechargPlace);
            rechargingObject = rechargeableInstance.GetComponent<Rechargeable>();

            if (rechargeBatteryCoroutine != null)
            {
                StopCoroutine(rechargeBatteryCoroutine);
            }
            rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(rechargingObject));
        }
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

    public void PlaceItemOnRecharger(Rechargeable rechargeableObj)
    {

    }

    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
