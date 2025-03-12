using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [Header("UI")]
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public ItemDataSO _item;
    [HideInInspector] public int _count = 1; //Para el stack, asi cada item cuenta 1
    [HideInInspector] public Transform _parentAfterDrag;


    public void Initialize(ItemDataSO newItem)
    {
        _item = newItem;
        image.sprite = newItem.icon;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = _count.ToString();
        bool textActive = _count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(_parentAfterDrag);

        // Verificar si el slot de origen estaba seleccionado y está vacío
        InventorySlot originalSlot = _parentAfterDrag.GetComponent<InventorySlot>();
        if (originalSlot != null && 
            Inventory.Instance.SelectedSlot !=
            Array.IndexOf(Inventory.Instance.inventorySlots, originalSlot) &&
            originalSlot.transform.childCount == 1)
        {
            Inventory.Instance.UnequipItem();
        }
    }
}
