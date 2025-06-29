using UnityEngine;

public class CraftingStation : MonoBehaviour, IInteractable, ILookable
{
    private Material StationMaterial;

    void Awake()
    {
        StationMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    public void Interact(PlayerInteractor interactor)
    {
        
    }
    public void OnLookEnter(PlayerInteractor interactor)
    {
        HoverUIManager.Instance.DisplayHoverUIOnILookable(this);
        StationMaterial.SetFloat("_HighlightStrength", 0.4f);
    }
    public void OnLookExit(PlayerInteractor interactor)
    {
        HoverUIManager.Instance.HideHoverUI();
        StationMaterial.SetFloat("_HighlightStrength", 0f);
    }
    private void Reset()
    {
        if (GetComponent<HoverInfo>() == null)
            gameObject.AddComponent<HoverInfo>();
    }

}
