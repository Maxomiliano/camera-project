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
            gameObject.SetActive(false);
        }
    }

    //Funcion para equipar el objeto en Hand
    public virtual void OnEquip(Transform handPosition)
    {
        
    }
    public virtual void OnUnequip()
    {

    }

    //Parametros: HandTransform, 
    //returns: el objeto ya equipado


    //Funcion para activar la funcionalidad el objeto cuando est� equipado
    public virtual void PrepareObject()
    {
        
    }

    //Funcion para desactivar la funcionalidad cuando est� equipado
    public virtual void UnprepareObject()
    {
        
    }

    //Funcion para usar el objeto cuando est� activo y equipado
    public virtual void UseObject()
    {

    }

    //O crear clase o interfaz que sea UsableObject que
}
