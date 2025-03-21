using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] ItemDataSO itemData;

    public ItemDataSO ItemData { get => itemData; }

    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null && inventory.AddItem(itemData))
        {
            Destroy(gameObject);
            //Aca va el return ItemData
        }
    }
    
    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
