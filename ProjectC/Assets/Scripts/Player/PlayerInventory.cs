using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public PlayerInput PlayerInput;
    public CanvasGroup InventoryCanvasGroup;
    public bool IsInventoryOpen = false;

    public void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        IsInventoryOpen = !IsInventoryOpen;

        if (IsInventoryOpen)
        {
            InventoryCanvasGroup.alpha = 1f;
            InventoryCanvasGroup.interactable = true;
            InventoryCanvasGroup.blocksRaycasts = true;

            PlayerInput.SwitchCurrentActionMap("UI");

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            InventoryCanvasGroup.alpha = 0f;
            InventoryCanvasGroup.interactable = false;
            InventoryCanvasGroup.blocksRaycasts = false;

            PlayerInput.SwitchCurrentActionMap("Player");

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    

}
