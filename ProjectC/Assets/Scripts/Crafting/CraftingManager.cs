using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    // void Start()
    // {
    //     Component myBlade = ComponentFactory.CreateComponent(
    //         ComponentType.Blade,
    //         MaterialType.Steel,
    //         2
    //     );
    //     Component myGuard = ComponentFactory.CreateComponent(
    //         ComponentType.Guard,
    //         MaterialType.Mythril,
    //         2
    //     );
    //     Component myHandle = ComponentFactory.CreateComponent(
    //         ComponentType.Handle,
    //         MaterialType.Wood,
    //         3
    //     );
    //     List<Component> myComponents = new List<Component>
    //     {
    //         myBlade,
    //         myGuard,
    //         myHandle
    //     };
    //     Equipment myEquipment = EquipmentFactory.CreateEquipmentItem(
    //         myComponents, EquipmentType.Sword, "Excalibur"
    //     );
    //     MaterialItem myMaterial = new MaterialItem(MaterialType.Steel, "Steel Bar", 25, 100);
    //     InstantiateItem(myMaterial);
    // }
    void InstantiateEquipment(Equipment equipment)
    {
        var components = equipment.Components;
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(equipment));
        foreach (var component in components)
        {
            GameObject prefabObject = Instantiate(ItemVisual.GetItemPrefab(component), prefab.transform.Find(component.Type.ToString()));
            prefabObject.GetComponentInChildren<MeshRenderer>().material = MaterialLibrary.Instance.Materials.First(item => item.name == component.MaterialType.ToString());
        }
    }

    void InstantiateComponent(Component component)
    {
        GameObject prefab = Instantiate(ItemVisual.GetItemPrefab(component));
        prefab.GetComponentInChildren<MeshRenderer>().material = MaterialLibrary.Instance.Materials.First(item => item.name == component.MaterialType.ToString());
    }

    void InstantiateItem(Item item)
    {
        if (item is Equipment or Component)
        {
            throw new ArgumentException("Use InstantiateComponent or InstantiateEquipment To spawn this");
        }
        Instantiate(ItemVisual.GetItemPrefab(item));
    }

}
