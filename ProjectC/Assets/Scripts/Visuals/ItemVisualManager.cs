using System;
using System.Linq;
using UnityEngine;

public class ItemVisualManager : MonoBehaviour
{
    public static ItemVisualManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ItemVisualResult CreateVisualWithCollider(Item item)
    {
        if (item is Equipment equipment)
            return CreateEquipmentVisual(equipment);
        if (item is Component component)
            return CreateComponentVisual(component);
        return CreateBasicItemVisual(item);
    }

    private ItemVisualResult CreateEquipmentVisual(Equipment equipment)
    {
        var components = equipment.Components;
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(equipment));

        foreach (var component in components)
        {
            Transform slot = prefab.transform.Find(component.Type.ToString());
            GameObject part = Instantiate(ItemVisual.GetItemPrefab(component), slot);
            part.GetComponentInChildren<MeshRenderer>().material =
                MaterialLibrary.Instance.Materials.First(m => m.name == component.MaterialType.ToString());
        }

        // Combine all meshes for collider
        MeshFilter[] meshFilters = prefab.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combine);

        return new ItemVisualResult(prefab, combinedMesh);
    }

    private ItemVisualResult CreateComponentVisual(Component component)
    {
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(component));
        prefab.GetComponentInChildren<MeshRenderer>().material =
            MaterialLibrary.Instance.Materials.First(m => m.name == component.MaterialType.ToString());

        Mesh mesh = prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        return new ItemVisualResult(prefab, mesh);
    }

    private ItemVisualResult CreateBasicItemVisual(Item item)
    {
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(item));
        Mesh mesh = prefab.GetComponentInChildren<MeshFilter>()?.sharedMesh;
        return new ItemVisualResult(prefab, mesh);
    }

}