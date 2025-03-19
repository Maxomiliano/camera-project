using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance : MonoBehaviour
{
    public ItemDataSO ItemData { get; private set; }
    public Dictionary<string, float> DynamicAttributes { get; private set; }

    public ItemInstance(ItemDataSO itemData)
    {
        ItemData = itemData;
        DynamicAttributes = new Dictionary<string, float>();

        if (itemData.HasBattery)
        {
            DynamicAttributes["BatteryLevel"] = 100f;
        }
    }

    public void SetAttribute(string key, float value)
    {
        DynamicAttributes[key] = Mathf.Clamp(value, 0f, 100f);
    }

    public float GetAttribute(string key)
    {
        return DynamicAttributes.ContainsKey(key) ? DynamicAttributes[key] : 0;
    }
}
//Esta clase contendria la referencia de ItemDataSO así tambien como los atributos dinámicos
//Es decir, batería, duración, etc. que no están definidos en el ItemDataSO