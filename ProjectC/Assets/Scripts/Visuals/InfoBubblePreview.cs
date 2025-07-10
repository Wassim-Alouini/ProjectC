using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBubblePreview : MonoBehaviour
{
    //Has a camera, an anchor, a RenderTexture and an Item
    //Spawns the item visual in the anchor
    //Outputs Render to the RenderTexture

    [SerializeField]
    private Camera previewCamera;

    [SerializeField]
    private Transform anchor;

    [SerializeField]
    private Vector2 textureSize;

    [SerializeField]
    private float rotationSpeed = 10f;

    public RenderTexture RenderOutput;
    public Item SourceItem;

    public void InitRenderOutput()
    {
        RenderOutput = new RenderTexture((int)textureSize.x, (int)textureSize.y, 16);
        RenderOutput.Create();
        previewCamera.targetTexture = RenderOutput;
    }

    private void Start()
    {
        InitRenderOutput();
    }

    private void Update()
    {
        RotateAnchor();
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

    public IEnumerator<RenderTexture> GetVisual()
    {
        // Clear previous visuals
        foreach (Transform child in anchor)
            Destroy(child.gameObject);

        // Create new visual
        var visual = ItemVisualManager.Instance.CreateVisualWithCollider(SourceItem).Visual;
        visual.transform.SetParent(anchor, false);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localRotation = Quaternion.identity;

        yield return null; // Wait for one frame

        // Calculate bounds
        Bounds bounds = GetBounds(visual);

        // Center the visual
        Vector3 worldCenter = bounds.center;
        Vector3 localOffset = anchor.InverseTransformPoint(worldCenter);
        visual.transform.localPosition = -localOffset;

        // Measure size
        float width = bounds.size.x;
        float height = bounds.size.y;
        float depth = bounds.size.z;
        float maxDimension = Mathf.Max(width, height, depth);

        // FOV math
        float verticalFOV = previewCamera.fieldOfView * Mathf.Deg2Rad;
        float aspect = previewCamera.aspect;
        float horizontalFOV = 2 * Mathf.Atan(Mathf.Tan(verticalFOV / 2) * aspect);

        float distanceByHeight = (height / 2) / Mathf.Tan(verticalFOV / 2);
        float distanceByWidth = (width / 2) / Mathf.Tan(horizontalFOV / 2);
        float baseDistance = Mathf.Max(distanceByHeight, distanceByWidth, depth);

        // 🔧 Extra boost for small objects
        float sizeNormalizationReference = 1.0f;
        float smallness = Mathf.Clamp01(sizeNormalizationReference / maxDimension);
        float scaleBoost = Mathf.Lerp(1.0f, 1.8f, smallness);

        float finalDistance = baseDistance * scaleBoost * 1.2f; // 1.2f = general padding

        // Position camera
        Vector3 camDirection = previewCamera.transform.forward;
        Vector3 target = anchor.TransformPoint(Vector3.zero); // Center
        previewCamera.transform.position = target - camDirection * finalDistance;
        previewCamera.transform.LookAt(target);

        yield return RenderOutput;
    }




    private void RotateAnchor()
    {
        anchor.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }





}
