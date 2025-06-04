using System;
using System.Collections.Generic;

public class Component : Item
{
    public ComponentType Type { get; private set; }
    public List<ComponentTag> Tags { get; private set; }
    public MaterialType MaterialType { get; set; }
    public int Level { get; set; }


    public Component(ComponentType type, List<ComponentTag> tags, string name, int level = 1)
    {
        Type = type;
        Tags = tags;
        Name = name ?? throw new ArgumentException(nameof(name), "Name cannot be null.");
        Level = level;
    
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
