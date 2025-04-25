using System.Collections;
using UnityEngine;

public class BatteryRecharger : MonoBehaviour, IInteractable
{
    [SerializeField] float timeToRecharge = 10f;
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] Transform rechargPlace;
    private Coroutine rechargeBatteryCoroutine;

    public void Interact()
    {
        ToolbarItem objectToRecharg = Inventory.Instance.GetSelectedItem();
        if (objectToRecharg == null) return;

        GrabbableObject grabbableObj = objectToRecharg.gameObject.GetComponent<GrabbableObject>();
        if (grabbableObj == null) return;

        ItemData itemData = grabbableObj.CurrentData;
        if (itemData == null) return;

        GameObject oldObject = objectToRecharg.gameObject;

        oldObject.transform.SetParent(rechargPlace);
        oldObject.transform.localPosition = Vector3.zero;
        oldObject.transform.localRotation = Quaternion.identity;

        Inventory.Instance.RemoveInventoryItemFromSlot(itemData);

        IRechargeable rechargeableObj = oldObject.GetComponent<ToolbarItem>() as IRechargeable;
        if (rechargeableObj == null) return;

        if (rechargeBatteryCoroutine != null)
        {
            StopCoroutine(rechargeBatteryCoroutine);
        }
        rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(rechargeableObj));
    }

    private IEnumerator RechargeBattery(IRechargeable rechargableObj)
    {
        if (rechargPlace.childCount == 0) yield break;

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
