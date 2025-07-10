using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryUIContainer;
    public GameObject InventoryIconPrefab;
    public GameObject InfoBubbleObject;
    private InfoBubble infoBubble;


    public static InventoryUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Inventory.Instance.OnInventoryChanged += RefreshInventoryUI;
        InitInventory();
        infoBubble = InfoBubbleObject.GetComponent<InfoBubble>();

    }
    public static void InitInventory()
    {
        Inventory.Instance.ClearInventory();
        Inventory.Instance.SetCapacity(20);
    }

    private void OnDestroy()
    {
        Inventory.Instance.OnInventoryChanged -= RefreshInventoryUI;
    }
    public void RefreshInventoryUI()
    {
        Debug.Log(Inventory.Instance.Items.Count + " Items in inventory");
        foreach (Transform child in InventoryUIContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Inventory.Instance.Items)
        {
            GameObject icon = Instantiate(InventoryIconPrefab, InventoryUIContainer.transform);
            icon.GetComponent<ItemIcon>().SourceItem = item;
        }
    }
    public void DisplayInfoBubble(Item sourceItem)
    {
        infoBubble.SourceItem = sourceItem;
        infoBubble.RefreshDisplay();
        InfoBubbleObject.SetActive(true);
    }
    public void HideInfoBubble()
    {
        InfoBubbleObject.SetActive(false);
    }


    



}
