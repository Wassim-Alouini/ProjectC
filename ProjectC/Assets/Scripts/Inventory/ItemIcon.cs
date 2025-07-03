using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    public Item SourceItem;
    public RawImage rawImage;

    private Coroutine _updateRoutine;

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
