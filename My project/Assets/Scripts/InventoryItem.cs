using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TMP_Text itemName;
    public TMP_Text currentChargeText;
    //public TMP_Text countText;

    [HideInInspector] public int _count = 1; //Para el stack, asi cada item cuenta 1
    [HideInInspector] public Transform _parentAfterDrag;
    private ItemData _currentData;

    public ItemData CurrentData { get => _currentData; set => _currentData = value; }

    //Set data
    public void Initialize(ItemData data)
    {
        _currentData = data;
        Refresh();
        //RefreshCount();        
    }

    public void Refresh()
    {
        image.sprite = _currentData.icon;
        itemName.text = _currentData.itemName;
        currentChargeText.text = $"{_currentData.currentBatteryAmmount} / {_currentData.maxBatteryAmmount}";
    }
    /*
    //Refresh
    public void RefreshCount()
    {
        countText.text = _count.ToString();
        bool textActive = _count > 1;
        if (_count < 1)
        {
            Destroy(gameObject);
        }
        countText.gameObject.SetActive(textActive);
    }
    */
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
            Inventory.Instance.DeselectItem();
        }
    }

    public ItemData RemoveFromInventory()
    {
        gameObject.SetActive(false);
        return _currentData; ;
    }
}
