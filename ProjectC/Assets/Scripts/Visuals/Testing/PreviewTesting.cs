using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PreviewTesting : MonoBehaviour
{
    public Camera previewCamera;
    public Transform previewAnchor;
    public Vector2 textureSize = new Vector2(256, 256);

    private RenderTexture renderTexture;
    public Material DisplayMaterial;

    private void Awake()
    {
        renderTexture = new RenderTexture((int)textureSize.x, (int)textureSize.y, 16);
        renderTexture.Create();
        previewCamera.targetTexture = renderTexture;
    }

    public IEnumerator<Texture2D> TexturizeAsync(Item item)
    {
        var visual = ItemVisualManager.Instance.CreateVisualWithCollider(item).Visual;
        visual.transform.SetParent(previewAnchor);
        visual.transform.localPosition = Vector3.zero;
        visual.transform.localRotation = Quaternion.identity;

        yield return null;

        RenderTexture.active = renderTexture;
        previewCamera.Render();

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        Destroy(visual);

        yield return tex;
    }

    private void Start()
    {
        //StartCoroutine(GeneratePreviewTexture());
    }

    public void Update()
    {
        var myDict = ItemTexturesCache.Instance.GetAll();
        if (myDict.Count == 0) return;
        DisplayMaterial.mainTexture = myDict.Values.Last();
    }

    private IEnumerator GeneratePreviewTexture()
    {
        Component myBlade = ComponentFactory.CreateComponent(
            ComponentType.Blade,
            MaterialType.Steel,
            2
        );
        Component myGuard = ComponentFactory.CreateComponent(
            ComponentType.Guard,
            MaterialType.Mythril,
            2
        );
        Component myHandle = ComponentFactory.CreateComponent(
            ComponentType.Handle,
            MaterialType.Wood,
            3
        );

        List<Component> myComponents = new List<Component>
        {
            myBlade,
            myGuard,
            myHandle
        };
        Equipment myEquipment = EquipmentFactory.CreateEquipmentItem(
            myComponents, EquipmentType.Sword, "Test Sword"
        );
        var coroutine = TexturizeAsync(myEquipment);
        yield return coroutine;
        Texture2D myTexture = coroutine.Current;
        DisplayMaterial.mainTexture = myTexture;
        Debug.Log("Texture generated: " + (myTexture != null));
    }

}
