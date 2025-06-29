using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    Camera mainCamera;
    ILookable currentLookingTarget;
    public float InteractionRange;
    public LayerMask InteractableLayer;

    private PlayerInputActions inputActions;


    void Awake()
    {
        mainCamera = Camera.main;
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.performed += ctx => TryInteract();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        TryLook();
    }

    public void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, InteractionRange, InteractableLayer))
        {
            IInteractable myInteractable = hit.collider.GetComponent<IInteractable>();
            if (myInteractable is null)
            {
                return;
            }
            myInteractable.Interact(this);
        }
    }
    public void TryLook()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, InteractionRange, InteractableLayer))
        {
            ILookable myLookable = hit.collider.GetComponent<ILookable>();

            if (myLookable != null)
            {
                if (myLookable != currentLookingTarget)
                {
                    currentLookingTarget?.OnLookExit(this);
                    currentLookingTarget = myLookable;
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
