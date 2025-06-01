using System;
using System.Collections.Generic;

public class Component
{
    public ComponentType Type { get; private set; }
    public List<ComponentTag> Tags { get; private set; }
    public string Name { get; private set; }


    public Component(ComponentType type, List<ComponentTag> tags, string name)
    {
        Type = type;
        Tags = tags;
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
