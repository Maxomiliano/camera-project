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
                ShowInteractUI();
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

    private void ShowInteractUI()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactCanvas.alpha = 1;
                //interactText.text = 
                interactCanvas.transform.position = Camera.main.WorldToScreenPoint(hit.point + Vector3.up * 0.5f);
                return;
            }
        }
    }
}
