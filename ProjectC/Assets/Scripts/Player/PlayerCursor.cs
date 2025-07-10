using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCursor : MonoBehaviour
{
    public PlayerInventory Inventory;
    public Canvas CrosshairCanvas;
    public RectTransform CrosshairImage;
    public RectTransform CrosshairCircle;

    public float scaleSpeed = 10f;

    private Vector3 targetScale = Vector3.one;


    public static PlayerCursor Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

        CrosshairCircle.localScale = Vector3.Lerp(CrosshairCircle.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }
    public void FocusedCursor()
    {
        targetScale = Vector3.one * 1.8f;
    }

    public void DefaultCursor()
    {
        targetScale = Vector3.one;
    }
}
