using UnityEngine;

public abstract class EquippableObject : MonoBehaviour
{
    public virtual void OnEquip(Transform handPosition)
    {

    }
    public virtual void OnUnequip()
    {

    }


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
}
