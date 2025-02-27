using UnityEngine;

public abstract class EquippableObject : MonoBehaviour
{
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
