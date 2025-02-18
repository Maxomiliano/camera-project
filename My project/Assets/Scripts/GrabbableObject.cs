using UnityEngine;

public class GrabbableObject : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("Objeto recogido");
        gameObject.SetActive(false);
    }
}
