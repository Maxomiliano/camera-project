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
                inventory.EquipedItem.OnPrimaryUse();
            }

            if (Mouse.current.rightButton.isPressed)
            {
                inventory.EquipedItem.OnSecondaryUse();
            }
            else
            {
                inventory.EquipedItem.OnSecondaryRelease();
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
