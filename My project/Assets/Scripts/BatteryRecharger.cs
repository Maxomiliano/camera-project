using System.Collections;
using UnityEngine;

public class BatteryRecharger : MonoBehaviour, IInteractable
{
    [SerializeField] float timeToRecharge = 10f;
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] Transform rechargPlace; 
    Inventory inventory;
    private Coroutine rechargeBatteryCoroutine;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void Interact()
    {
        ToolbarItem objectToRecharg = inventory.GetSelectedItem();
        if (objectToRecharg == null) return;

        GrabbableObject grabbableObj = objectToRecharg.gameObject.GetComponent<GrabbableObject>();
        if (grabbableObj == null) return;

        ItemInstance itemInstance = grabbableObj.ItemInstance;
        if(itemInstance == null) return;

        GameObject oldObject = objectToRecharg.gameObject;

        inventory.RemoveItem(itemInstance);
        inventory.DeselectItem();

        GameObject rechargeableInstance = Instantiate(oldObject, rechargPlace);
        Rechargeable newRecargeable = rechargeableInstance.GetComponent<Rechargeable>();

        if (newRecargeable != null)
        {
            float batteryLevel = itemInstance.GetAttribute("BatteryLevel");
            newRecargeable.SetBatteryLevel(batteryLevel);
        }

        if (rechargeBatteryCoroutine != null)
        {
            StopCoroutine(rechargeBatteryCoroutine);
        }
        rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(newRecargeable, itemInstance));
    }

    private IEnumerator RechargeBattery(Rechargeable rechargableObj, ItemInstance itemInstance)
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
