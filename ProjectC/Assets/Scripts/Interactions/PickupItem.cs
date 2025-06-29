using System;
using System.Data;
using System.Linq;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable, ILookable
{
    public Item sourceItem;
    public MeshCollider targetCollider;

    private Material[] materials;

    GameObject InstantiateEquipment(Equipment equipment)
    {
        var components = equipment.Components;
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(equipment));
        var finalMeshForCollider = new Mesh();
        foreach (var component in components)
        {
            GameObject prefabObject = Instantiate(ItemVisual.GetItemPrefab(component), prefab.transform.Find(component.Type.ToString()));
            prefabObject.GetComponentInChildren<MeshRenderer>().material = MaterialLibrary.Instance.Materials.First(item => item.name == component.MaterialType.ToString());
        }
        MeshFilter[] meshFilters = prefab.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];


        for (int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combine);

        targetCollider.sharedMesh = combinedMesh;
        targetCollider.convex = true;

        return prefab;
    }

    GameObject InstantiateComponent(Component component)
    {
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(component));

        var renderer = prefab.GetComponentInChildren<MeshRenderer>();
        renderer.material = MaterialLibrary.Instance.Materials
            .First(item => item.name == component.MaterialType.ToString());

        var meshFilter = prefab.GetComponentInChildren<MeshFilter>();
        var mesh = meshFilter.sharedMesh;

        targetCollider.sharedMesh = mesh;
        targetCollider.convex = true;

        return prefab;
    }

    GameObject InstantiateItem(Item item)
    {
        if (item is Equipment or Component)
        {
            throw new ArgumentException("Use InstantiateComponent or InstantiateEquipment to spawn this");
        }

        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(item));

        var meshFilter = prefab.GetComponentInChildren<MeshFilter>();
        if (meshFilter != null)
        {
            var mesh = meshFilter.sharedMesh;
            targetCollider.sharedMesh = mesh;
            targetCollider.convex = true;
        }

        return prefab;
    }

    public void InstantiatePickupItem()
    {
        HoverInfo hoverInfo = GetComponent<HoverInfo>();
        hoverInfo.DisplayName = sourceItem.Name;
        hoverInfo.Description = "Item";
        hoverInfo.Action = "Press [E] to pickup";
        GameObject item;
        if (sourceItem is Equipment equipment)
        {
            item = InstantiateEquipment(equipment);
        }
        else
        {
            if (sourceItem is Component component)
            {
                item = InstantiateComponent(component);
            }
            else
            {
                item = InstantiateItem(sourceItem);
            }
        }
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
        materials = GetComponentsInChildren<MeshRenderer>()
        .SelectMany(r => r.materials)
        .Where(m => m != null)
        .ToArray();
        Debug.Log(materials.Length);
    }

    private void HighlightInteractable()
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat("_HighlightStrength", 0.4f);
        }
    }

    private void UnHighlightInteractable()
    {
        foreach (Material mat in materials)
        {
            mat.SetFloat("_HighlightStrength", 0f);
        }
    }

    public void Interact(PlayerInteractor interactor)
    {
        //Debug.Log("Picking up " + name);
    }

    public void OnLookEnter(PlayerInteractor interactor)
    {
        Debug.Log("Looking at " + name);
        HoverUIManager.Instance.DisplayHoverUIOnILookable(this);
        HighlightInteractable();
    }

    public void OnLookExit(PlayerInteractor interactor)
    {
        //Debug.Log("Not looking at " + name);
        HoverUIManager.Instance.HideHoverUI();
        UnHighlightInteractable();
    }

    private void Reset()
    {
        if (GetComponent<HoverInfo>() == null)
            gameObject.AddComponent<HoverInfo>();
    }
}
