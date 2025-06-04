using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemVisual : MonoBehaviour
{
    public static GameObject GetItemPrefab(Item sourceItem)
    {
        string lookupName = "";
        if (sourceItem is ResourceItem resourceItem)
        {
            lookupName = resourceItem.ResourceType.ToString();
        }
        else if (sourceItem is Component component)
        {
            lookupName = $"{component.Type}{component.Level}";
        }
        else if (sourceItem is Equipment equipment)
        {
            lookupName = equipment.Type.ToString();
        }
        else if (sourceItem is MaterialItem materialItem)
        {
            lookupName = materialItem.MaterialType.ToString();
        }
        Debug.Log($"Looking up prefab for item: {lookupName}");
        return ItemPrefabLibrary.Instance.ItemPrefabs.First(item => item.name == lookupName);
    }





}
