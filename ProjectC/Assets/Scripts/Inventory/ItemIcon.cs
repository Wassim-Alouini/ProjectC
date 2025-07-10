using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Item SourceItem;
    public RawImage rawImage;

    private Coroutine _updateRoutine;

    public Image backgroundImage;

    public Color normalColor = new Color(255, 255, 255, 0.8f);
    public Color hoverColor = new Color(1f, 1f, 0.5f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (backgroundImage != null)
            backgroundImage.color = hoverColor;
        PlayerCursor.Instance.FocusedCursor();
        InventoryUI.Instance.DisplayInfoBubble(SourceItem);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (backgroundImage != null)
            backgroundImage.color = normalColor;
        PlayerCursor.Instance.DefaultCursor();
        InventoryUI.Instance.HideInfoBubble();
    }

    private void Start()
    {
        if (Inventory.Instance != null)
            Inventory.Instance.OnInventoryChanged += OnInventoryChanged;

        StartListening();
    }

    private void OnDestroy()
    {
        if (Inventory.Instance != null)
            Inventory.Instance.OnInventoryChanged -= OnInventoryChanged;

        StopListening();
    }

    private void OnInventoryChanged()
    {
        StartListening();
    }

    private void StartListening()
    {
        if (_updateRoutine != null)
            StopCoroutine(_updateRoutine);

        _updateRoutine = StartCoroutine(WaitForTextureAndUpdate());
    }

    private void StopListening()
    {
        if (_updateRoutine != null)
        {
            StopCoroutine(_updateRoutine);
            _updateRoutine = null;
        }
    }

    private IEnumerator WaitForTextureAndUpdate()
    {
        if (SourceItem == null || rawImage == null || ItemTexturesCache.Instance == null)
            yield break;

        Texture2D tex;

        while (!ItemTexturesCache.Instance.TryGet(SourceItem, out tex))
            yield return null;

        rawImage.texture = tex;
    }
}
