using System;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;


    void Start()
    {
        StarterAssets.StarterAssetsInputs.OnInteractTrigger += TryInteract;
    }

    private void TryInteract()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
