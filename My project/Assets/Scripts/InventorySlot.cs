using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor;
    public Color notSelectedColor;

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem._parentAfterDrag = transform;

            // Verificar si el slot seleccionado está vacío y equipar el item
            if (Inventory.Instance.SelectedSlot == Array.IndexOf(Inventory.Instance.inventorySlots, this))
            {
                Inventory.Instance.ShowItemInHand(inventoryItem.CurrentData, Inventory.Instance.handPosition);
            }
        }
    }
}
