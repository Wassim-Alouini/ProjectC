using System.Linq;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable, ILookable
{
    public Item sourceItem;
    public MeshCollider targetCollider;

    private Material[] materials;

    public void InstantiatePickupItem()
    {
        HoverInfo hoverInfo = GetComponent<HoverInfo>();
        hoverInfo.DisplayName = sourceItem.Name;
        hoverInfo.Description = "Item";
        hoverInfo.Action = "Press [E] to pickup";

        var result = ItemVisualManager.Instance.CreateVisualWithCollider(sourceItem);
        GameObject visual = result.Visual;

        visual.transform.SetParent(transform);
        visual.transform.localPosition = Vector3.zero;

        if (result.ColliderMesh != null)
        {
            targetCollider.sharedMesh = result.ColliderMesh;
            targetCollider.convex = true;
        }

        materials = visual.GetComponentsInChildren<MeshRenderer>()
            .SelectMany(r => r.materials)
            .Where(m => m != null)
            .ToArray();
    }


    private void HighlightInteractable()
    {
        foreach (Material mat in materials)
            mat.SetFloat("_HighlightStrength", 0.4f);
    }

    private void UnHighlightInteractable()
    {
        foreach (Material mat in materials)
            mat.SetFloat("_HighlightStrength", 0f);
    }

    public void Interact(PlayerInteractor interactor)
    {
        Pickup();
    }

    private void Pickup()
    {
        Destroy(gameObject);
        Inventory.Instance.AddItem(sourceItem);
        Debug.Log("PICKING UP");
    }

    public void OnLookEnter(PlayerInteractor interactor)
    {
        HoverUIManager.Instance.DisplayHoverUIOnILookable(this);
        HighlightInteractable();
        PlayerCursor.Instance.FocusedCursor();
    }

    public void OnLookExit(PlayerInteractor interactor)
    {
        HoverUIManager.Instance.HideHoverUI();
        UnHighlightInteractable();
        PlayerCursor.Instance.DefaultCursor();
    }

    private void Reset()
    {
        if (GetComponent<HoverInfo>() == null)
            gameObject.AddComponent<HoverInfo>();
    }
}
