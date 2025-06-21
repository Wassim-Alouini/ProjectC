using System.Collections.Generic;

public class EquipmentFactory
{
    public static Equipment CreateEquipmentItem(List<Component> components,EquipmentType type, string name)
    {
        List<ComponentTag> tags = new List<ComponentTag>();
        foreach (var component in components)
        {
            foreach (var tag in component.Tags)
            {
                if (tags.Contains(tag))
                {
                    continue;
                }
                else
                {
                    tags.Add(tag);
                }
            }
            
        }
        Equipment equipment = new Equipment(tags,type,name);
        equipment.Components = components;
        return equipment;
    }
}