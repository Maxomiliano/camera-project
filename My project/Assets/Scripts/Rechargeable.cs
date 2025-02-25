using UnityEngine;

public abstract class Rechargeable : MonoBehaviour
{
    [SerializeField] protected float maxBatteryPercentage = 100f;
    [SerializeField] protected float currentBatteryPercentage;

    public float CurrentBatteryPercentage => currentBatteryPercentage;
    public float MaxBatteryPercentage  => maxBatteryPercentage;

    public virtual void RechargeBattery(float ammount)
    {
        currentBatteryPercentage = Mathf.Min(currentBatteryPercentage + ammount, maxBatteryPercentage);
    }

    public virtual void DecreaseBattery(float ammount)
    {
        currentBatteryPercentage = Mathf.Max(currentBatteryPercentage - ammount, 0f);
        {
            if (currentBatteryPercentage <= 0)
            {
                Debug.Log("You have to recharge the battery");
            }
            else 
            {
                Debug.Log($"Battery decreased by {ammount}");
            }
        }
    }
}
