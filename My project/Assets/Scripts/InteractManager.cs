using System;
using TMPro;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private CanvasGroup interactCanvas;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAction;

    void Start()
    {
        StarterAssets.StarterAssetsInputs.OnInteractTrigger += TryInteract;
        interactCanvas.alpha = 0;
    }

    private void Update()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactCanvas.transform.position = Camera.main.WorldToScreenPoint(hit.point + Vector3.up * 0.5f);
                ShowInteractUI(interactable);
                return;
            }
        }
        interactCanvas.alpha = 0;
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

    private void ShowInteractUI(IInteractable interactable)
    {
        ObjectIdentifier objectIdentifier = interactable.GetIdenfier();
        interactCanvas.alpha = 1;
        if (objectIdentifier != null)
        {
            interactText.text = objectIdentifier.ObjectName;
            interactAction.text = objectIdentifier.ObjectAction;
        }
        else
        {
            interactText.text = "";
            interactAction.text = "";
        }
        return;
    }
}