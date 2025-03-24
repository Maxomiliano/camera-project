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

        ItemData itemData = grabbableObj.CurrentData;
        if (itemData == null) return;

        GameObject oldObject = objectToRecharg.gameObject;

        inventory.RemoveItem(itemData);
        inventory.DeselectItem();

        GameObject rechargeableInstance = Instantiate(oldObject, rechargPlace);
        IRechargeable rechargeableObj = rechargeableInstance.GetComponent<ToolbarItem>() as IRechargeable;

        
        if (rechargeBatteryCoroutine != null)
        {
            StopCoroutine(rechargeBatteryCoroutine);
        }
        rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(rechargeableObj));
    }

    private IEnumerator RechargeBattery(IRechargeable rechargableObj)
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

    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
