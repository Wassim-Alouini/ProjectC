using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Camera mainCamera;
    private ILookable currentLookingTarget;

    public float InteractionRange = 3f;
    public LayerMask InteractableLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        TryLook();
    }

    public void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            TryInteract();
        }

    }

    private void TryInteract()
    {
        //Debug.Log("TryInteract");
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, InteractionRange, InteractableLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.Interact(this);
        }
    }

    private void TryLook()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, InteractionRange, InteractableLayer))
        {
            var lookable = hit.collider.GetComponent<ILookable>();
            if (lookable != null)
            {
                if (lookable != currentLookingTarget)
                {
                    currentLookingTarget?.OnLookExit(this);
                    currentLookingTarget = lookable;
                    currentLookingTarget.OnLookEnter(this);
                }
                return;
            }
        }

        if (currentLookingTarget != null)
        {
            currentLookingTarget.OnLookExit(this);
            currentLookingTarget = null;
        }
    }
}
