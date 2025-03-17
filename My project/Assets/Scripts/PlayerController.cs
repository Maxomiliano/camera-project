using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform handPosition;


    private void Update()
    {
        CheckIfUIActive();
        if (inventory.EquipedItem != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                inventory.EquipedItem.UseObject();
            }

            if (Mouse.current.rightButton.isPressed)
            {
                inventory.EquipedItem.PrepareObject();
            }
            else
            {
                inventory.EquipedItem.UnprepareObject();
            }
        }

    }

    private void CheckIfUIActive()
    {
        if (UIManager.Instance.IsAnyPanelOpen())
        {
            GetComponent<FirstPersonController>().enabled = false;
        }
        else
        {
            GetComponent<FirstPersonController>().enabled = true;
        }
    }
}
