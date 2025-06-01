using System;
using System.Collections.Generic;

public class Equipment : Item
{
    public List<ComponentTag> Tags { get; private set; }

    public Equipment(List<ComponentTag> tags, string name)
    {
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