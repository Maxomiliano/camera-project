using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{
    [SerializeField] ObjectIdentifier objectIdentifier;

    public ObjectIdentifier GetIdenfier()
    {
        return objectIdentifier;
    }

    public void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();
        if (inventory != null && inventory.AddItem(this))
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //Funcion para equipar el objeto en Hand
    //Parametros: HandTransform, 
    //returns: el objeto ya equipado
    public virtual void OnEquip(Transform handPosition)
    {

    }
    public virtual void OnUnequip()
    {

    }


    //Funcion para activar la funcionalidad el objeto cuando esté equipado
    public virtual void PrepareObject()
    {
        
    }

    //Funcion para desactivar la funcionalidad cuando esté equipado
    public virtual void UnprepareObject()
    {
        
    }

    //Funcion para usar el objeto cuando esté activo y equipado
    public virtual void UseObject()
    {

    }
}
