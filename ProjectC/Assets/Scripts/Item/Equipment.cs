using System;
using System.Collections.Generic;

public class Equipment : Item
{
    public EquipmentType Type { get; private set; }
    public List<Component> Components { get; set; }
    public List<ComponentTag> Tags { get; private set; }

    public Equipment(List<ComponentTag> tags,EquipmentType type, string name)
    {
        Tags = tags;
        Type = type;
        Name = name ?? throw new ArgumentException(nameof(name), "Name cannot be null.");
    }

    public void addTag(ComponentTag tag)
    {
        Tags.Add(tag);
    }
    public void RemoveTag(ComponentTag tag)
    {
        Tags.Remove(tag);
    }

}