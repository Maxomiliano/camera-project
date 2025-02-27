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
        EquippableObject objectToRecharg = inventory.GetEquippedItem();
        if (objectToRecharg == null) return;

        Rechargeable rechargeableObj = objectToRecharg.gameObject.GetComponent<Rechargeable>();
        if(rechargeableObj == null) return;


        inventory.UnequipItem();
        objectToRecharg.transform.SetParent(rechargPlace);
        objectToRecharg.transform.localPosition = Vector3.zero;
        if (rechargeBatteryCoroutine != null)
        {
            StopCoroutine(rechargeBatteryCoroutine);
        }
        rechargeBatteryCoroutine = StartCoroutine(RechargeBattery(rechargeableObj));
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
