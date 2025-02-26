using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform handPosition;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GrabbableObject obj = inventory.GetItem(0);
            if (obj != null)
            {
                inventory.EquipItem(obj, handPosition);
            }
        }
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
}
