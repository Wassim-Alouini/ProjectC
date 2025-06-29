using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionUI : MonoBehaviour
{
    public UIDocument myDocument;
    public float xScale;


    void Start()
    {
        StartCoroutine(LogCanvasSize());
    }

    IEnumerator LogCanvasSize()
    {
        yield return new WaitForEndOfFrame();

        var root = myDocument.rootVisualElement;

        float width = root.resolvedStyle.width;
        float height = root.resolvedStyle.height;

        float ratio = height / width;

        transform.localScale = new Vector3(xScale, xScale * ratio);
    }

}
