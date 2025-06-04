using System.Collections.Generic;

public class EquipmentFactory
{
    public static Equipment CreateEquipmentItem(List<Component> components,EquipmentType type, string name)
    {
        List<ComponentTag> tags = new List<ComponentTag>();
        foreach (var component in components)
        {
            tags.AddRange(component.Tags);
        }
        Equipment equipment = new Equipment(tags,type,name);
        equipment.Components = components;
        return equipment;
    }
}