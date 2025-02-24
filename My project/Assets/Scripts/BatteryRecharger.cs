using UnityEngine;

public class BatteryRecharger : MonoBehaviour, IInteractable
{
    [SerializeField] float timeToRecharge = 10f;
    private float rechargeAmmount;
    private float maxBatteryPercentage = 100;
    private IRechargeable rechargeableObject;
    Inventory inventory;

    private void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public void Interact()
    {
        GrabbableObject objectToRecharg = inventory.GetItem(0);
        if (objectToRecharg != null && objectToRecharg is IRechargeable)
        {
            rechargeableObject = objectToRecharg as IRechargeable;
        }
        RechargeBattery(rechargeableObject);
        Debug.Log("Battery Recharging");
    }

    private void RechargeBattery(IRechargeable rechargableObj)
    {
        if (rechargeableObject == null)
        {
            Debug.LogError("Intentando cargar objeto nulo");
            return;
        }
        rechargableObj.RechargeBattery(maxBatteryPercentage / timeToRecharge * Time.deltaTime);
        //rechargeAmmount += timeToRecharge * Time.deltaTime;
    }
}
