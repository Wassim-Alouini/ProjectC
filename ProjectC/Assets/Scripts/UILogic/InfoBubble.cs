using System.Collections;
using System.Runtime.Serialization.Formatters;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class InfoBubble : MonoBehaviour
{

    //Has a RawImage, an InfoBubblePreview, a SourceItem
    //Applies SourceItem on InfoBubblePreview and displays RenderOutput into RawImage
    [SerializeField]
    private RawImage display;

    [SerializeField]
    private InfoBubblePreview infoBubblePreview;

    public Item SourceItem;

    public Texture2D Transparent;

    [SerializeField]
    private TextMeshProUGUI ItemName;
    [SerializeField]
    private TextMeshProUGUI ItemType;
    [SerializeField]
    private TextMeshProUGUI ItemTraits;

    private void OnEnable()
    {
        StartCoroutine(RefreshDisplay());
    }

    public IEnumerator RefreshDisplay()
    {
        ItemName.text = "";
        ItemTraits.text = "";
        ItemType.text = "";
        display.texture = Transparent;
        ItemName.text = SourceItem.Name;
        if (SourceItem is Equipment eq)
        {
            ItemType.text = "Equipment";
            foreach (var tag in eq.Tags)
            {
                ItemTraits.text += $"\n {tag}";
            }
        }
        else if (SourceItem is Component comp)
        {
            ItemType.text = "Component";
            foreach (var tag in comp.Tags)
            {
                ItemTraits.text += $"\n {tag}";
            }
        }
        else if (SourceItem is MaterialItem)
        {
            ItemType.text = "Material";
        }

        yield return new WaitForSeconds(0.2f);
        infoBubblePreview.SourceItem = SourceItem;
        var coroutine = infoBubblePreview.GetVisual();
        yield return coroutine;
        display.texture = coroutine.Current;
    }

}
