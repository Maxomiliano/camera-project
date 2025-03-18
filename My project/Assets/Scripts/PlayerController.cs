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
        if (inventory.ToolbarItem != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                inventory.ToolbarItem.OnPrimaryUse();
            }

            if (Mouse.current.rightButton.isPressed)
            {
                inventory.ToolbarItem.OnSecondaryUse();
            }
            else
            {
                inventory.ToolbarItem.OnSecondaryRelease();
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
