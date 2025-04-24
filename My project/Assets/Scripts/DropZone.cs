using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (item != null)
        {
            Debug.Log($"Item {item.CurrentData.itemName} dropeado ");
        }
    }
}
