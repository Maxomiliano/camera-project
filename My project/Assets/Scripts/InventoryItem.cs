using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemDataSO item;

    [Header("UI")]
    public Image _image;

    [HideInInspector] public Transform parentAfterDrag;

    private void Start()
    {
        Initialize(item);
    }

    public void Initialize(ItemDataSO newItem)
    {
        _image.sprite = newItem.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
