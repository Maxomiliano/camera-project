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
    public TMP_Text countText;

    [HideInInspector] public int _count = 1; //Para el stack, asi cada item cuenta 1
    [HideInInspector] public Transform _parentAfterDrag;
    private ItemData _currentData;

    public ItemData CurrentData { get => _currentData; set => _currentData = value; }

    //Set data
    public void Initialize(ItemData data)
    {
        _currentData = data;
        Refresh();
        RefreshCount();        
    }

    [ContextMenu("Refresh")]
    public void Refresh()
    {
        image.sprite = _currentData.icon;
        itemName.text = _currentData.itemName;
        currentChargeText.text = $"{_currentData.currentBatteryAmmount} / {_currentData.maxBatteryAmmount}";
    }
    
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
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        _parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        Debug.Log("Comenzando Drag and Drop");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Sigue al cursor del mouse.
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        Transform slotUnderPointer = eventData.pointerEnter != null ? eventData.pointerEnter.transform : _parentAfterDrag;
        if (slotUnderPointer != null && slotUnderPointer.CompareTag("DropZone"))
        {
            Inventory.Instance.PopItem(_currentData);
            transform.SetParent(_parentAfterDrag);
            Debug.Log($"Objeto dropeado en la zona: {slotUnderPointer.name}");;
        }
        // Verificar si el slot de origen estaba seleccionado y está vacío
        else if (slotUnderPointer != null && slotUnderPointer.GetComponent<InventorySlot>() != null)
        {
            transform.SetParent(slotUnderPointer);
            Debug.Log($"Objeto movido al slot: {slotUnderPointer.name}");
        }
        else
        {
            transform.SetParent(_parentAfterDrag);
            Debug.Log($"Objeto devuelto al slot original: {_parentAfterDrag.name}");
        }
        transform.localPosition = Vector3.zero;
    }

    public ItemData RemoveFromInventory()
    {
        gameObject.SetActive(false);
        return _currentData; ;
    }
}
