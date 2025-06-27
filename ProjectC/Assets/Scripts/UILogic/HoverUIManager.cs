using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverUIManager : MonoBehaviour
{

    public static HoverUIManager Instance;
    public Camera mainCamera;

    public Vector3 Offset;

    [SerializeField] private float fadeDuration = 0.25f;
    private Coroutine fadeCoroutine;
    public RectTransform UIContainer;
    private HoverUIManager()
    { }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void Start()
    {
        mainCamera = Camera.main;
        HideUI();

    }
    public CanvasGroup HoverUICanvasGroup;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI ActionText;


    public void ShowUI()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(HoverUICanvasGroup, 1f));

        LayoutRebuilder.ForceRebuildLayoutImmediate(UIContainer);
    }

    public void HideUI()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeCanvasGroup(HoverUICanvasGroup, 0f));
    }

    public void DisplayHoverUIOnILookable(MonoBehaviour mono)
    {
        HoverInfo info = mono.GetComponent<HoverInfo>();
        NameText.text = info.DisplayName;
        DescriptionText.text = info.Description;
        ActionText.text = info.Action;
        HoverUICanvasGroup.transform.position = GetPlacingPosition(mono.GetComponent<BoxCollider>());//mono.transform.position + Vector3.up;
        ShowUI();
    }

    public Vector3 GetPlacingPosition(BoxCollider collider)
    {
        Bounds bounds = collider.bounds;
        var height = bounds.size.y;


        if (height <= 3)
        {
            return bounds.center + Vector3.up * bounds.extents.y + Vector3.up * 0.5f;
        }
        else
        {
            return bounds.center + mainCamera.transform.right * bounds.extents.x + Vector3.right;
        }
    }

    public void HideHoverUI()
    {
        HideUI();
    }

    private void FixedUpdate()
    {
        HoverUICanvasGroup.transform.LookAt(mainCamera.transform);
        HoverUICanvasGroup.transform.Rotate(0, 180, 0);
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup group, float targetAlpha)
    {
        float startAlpha = group.alpha;
        float time = 0f;

        if (targetAlpha > 0)
        {
            group.blocksRaycasts = true;
            group.interactable = true;
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            group.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        group.alpha = targetAlpha;

        if (targetAlpha == 0f)
        {
            group.blocksRaycasts = false;
            group.interactable = false;

            NameText.text = "";
            DescriptionText.text = "";
            ActionText.text = "";
        }
    }


}
