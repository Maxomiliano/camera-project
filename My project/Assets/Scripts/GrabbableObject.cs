using UnityEngine;

public class GrabbableObject : EquippableObject, IInteractable
{
    [SerializeField] ObjectIdentifier objectIdentifier;
    [SerializeField] ItemDataSO itemData;   


    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null && inventory.AddItem(this))
        {
            Destroy(gameObject);
        }
    }
    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }
}
