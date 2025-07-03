using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject InventoryUIContainer;
    public GameObject InventoryIconPrefab;

    void Awake()
    {
        Inventory.Instance.OnInventoryChanged += RefreshInventoryUI;
        InitInventory();
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



}
