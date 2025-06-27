using UnityEngine;

public class CraftingStation : MonoBehaviour, IInteractable, ILookable
{
    public Material StationMaterial;
    private Color BaseColor;
    public Color LookColor;
    void Awake()
    {
        StationMaterial = GetComponentInChildren<MeshRenderer>().material;
        BaseColor = StationMaterial.color;
    }

    public void Interact(PlayerInteractor interactor)
    {
        //Debug.Log("Interacting with" + name);
    }

    public void OnLookEnter(PlayerInteractor interactor)
    {
        StationMaterial.color = LookColor;
        //Display Hover UI
        HoverUIManager.Instance.DisplayHoverUIOnILookable(this);
       //Debug.Log("Looking at" + name);
    }

    public void OnLookExit(PlayerInteractor interactor)
    {
        StationMaterial.color = BaseColor;
        //Hide Hover UI
        HoverUIManager.Instance.HideHoverUI();
        //Debug.Log("Stopped looking at" + name);
    }


    //Waiting to find a better solution not involving writing this piece of code on every ILookable
    private void Reset()
    {
        if (GetComponent<HoverInfo>() == null)
            gameObject.AddComponent<HoverInfo>();
    }

}
