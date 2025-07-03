using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheTextures : MonoBehaviour
{
    public Camera previewCamera;
    public Transform previewAnchor;
    public Vector2 textureSize = new Vector2(256, 256);

    private RenderTexture renderTexture;

    private void Start()
    {
        renderTexture = new RenderTexture((int)textureSize.x, (int)textureSize.y, 16);
        renderTexture.Create();
        previewCamera.targetTexture = renderTexture;

        if (Inventory.Instance != null)
        {
            Inventory.Instance.OnInventoryChanged += OnInventoryChanged;
        }
    }

    private void OnInventoryChanged()
    {
        StopAllCoroutines(); 
        StartCoroutine(CacheAllInventoryTextures());
    }


    private Bounds GetBounds(GameObject visual)
    {
        var renderers = visual.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds(visual.transform.position, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        foreach (var rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }
        return bounds;
    }
    public IEnumerator<Texture2D> TexturizeAsync(Item item)
    {
        // Create visual
        var visual = ItemVisualManager.Instance.CreateVisualWithCollider(item).Visual;
        visual.transform.SetParent(previewAnchor, false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localRotation = Quaternion.identity;

        yield return null; // allow renderers to initialize

        // Calculate combined bounds of all renderers
        Bounds bounds = GetBounds(visual);

        // Recenter the visual based on bounds center
        Vector3 offset = bounds.center - previewAnchor.position;
        visual.transform.localPosition = -offset;

        // Apply Minecraft-style angled rotation
        visual.transform.localRotation = Quaternion.Euler(45f, -45f, 0f);

        // Recalculate bounds again after rotation
        bounds = GetBounds(visual);

        // Adjust camera to fit item size
        float maxSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float distance = maxSize * 1.8f; // You can tweak multiplier if too close/far
        Vector3 camPos = bounds.center - previewCamera.transform.forward * distance;
        previewCamera.transform.position = camPos;
        previewCamera.transform.LookAt(bounds.center);

        yield return null;

        // Render to texture
        RenderTexture.active = previewCamera.targetTexture;
        previewCamera.Render();

        Texture2D tex = new Texture2D(previewCamera.targetTexture.width, previewCamera.targetTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, previewCamera.targetTexture.width, previewCamera.targetTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        Destroy(visual);

        yield return tex;
    }

    public IEnumerator CacheAllInventoryTextures()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory not initialized.");
            yield break;
        }
        if (ItemTexturesCache.Instance == null)
        {
            Debug.LogWarning("ItemTexturesCache not initialized.");
            yield break;
        }

        HashSet<Item> inventoryItems = new HashSet<Item>(Inventory.Instance.Items);

        foreach (Item item in inventoryItems)
        {
            if (ItemTexturesCache.Instance.TryGet(item, out _))
                continue;

            var coroutine = TexturizeAsync(item);
            yield return coroutine;
            Texture2D tex = coroutine.Current;

            if (tex != null)
            {
                ItemTexturesCache.Instance.Add(item, tex);
            }
        }
    }


}
