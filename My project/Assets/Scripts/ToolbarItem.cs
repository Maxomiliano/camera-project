using UnityEngine;

public abstract class ToolbarItem : MonoBehaviour
{
    //Cuando el objeto está seleccionado en la toolbar
    public virtual void OnToolbarSelected(Transform handPosition)
    {

    }

    //Cuando dejo de seleccionar el objeto en la toolbar
    public virtual void OnToolbarDeselected()
    {

    }

    //Funcion para usar el objeto cuando esté seleccionado en la hotbar
    public virtual void OnPrimaryUse()
    {

    }

    //Funcion para activar la funcionalidad el objeto cuando esté equipado
    public virtual void OnSecondaryUse()
    {

    }

    //Funcion para desactivar la funcionalidad cuando esté equipado
    public virtual void OnSecondaryRelease()
    {

    }
}
