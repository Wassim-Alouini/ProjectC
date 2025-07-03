using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    public PlayerInventory Inventory;
    public Canvas CrosshairCanvas;
    public RectTransform CrosshairImage;
    void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        if (Inventory.IsInventoryOpen)
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)CrosshairCanvas.transform,
                Mouse.current.position.ReadValue(),
                null,
                out mousePos
            );

            CrosshairImage.anchoredPosition = mousePos;
        }
        else
        {
            CrosshairImage.anchoredPosition = Vector2.zero;
        }
    }
}
