using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null && inventory.AddItem(this))
        { 
            gameObject.SetActive(false);
        }
    }    
}
