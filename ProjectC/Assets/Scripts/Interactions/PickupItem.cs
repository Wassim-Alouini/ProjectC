using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable, ILookable
{
    public void Interact(PlayerInteractor interactor)
    {
        Debug.Log("Picking up " + name);
    }

    public void OnLookEnter(PlayerInteractor interactor)
    {
        throw new System.NotImplementedException();
    }

    public void OnLookExit(PlayerInteractor interactor)
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
