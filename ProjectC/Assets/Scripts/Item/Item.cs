using System;
using UnityEngine;

public abstract class Item
{
    public string Name { get; protected set; }
    public int BaseCost { get; protected set; }
    public int BasePower { get; protected set; }
    public ItemRarity Rarity { get; protected set; }
    // public Item(ItemType type, string name, int baseCost, int basePower, ItemRarity rarity, MaterialType? materialType = null, ResourceType? resourceType = null)
    // {
    //     Type = type;
    //     Name = name;
    //     BaseCost = baseCost;
    //     BasePower = basePower;
    //     Rarity = rarity;
    //     if (materialType == null)
    //     {
    //         if (resourceType == null)
    //         {
    //             return;
    //         }
    //         else
    //         {
    //             ResourceType = resourceType;
    //         }
    //     }
    //     else
    //     {
    //         if (resourceType != null)
    //         {
    //             throw new ArgumentException("An item cannot be both a material and a resource.");
    //         }
    //         else
    //         {
    //             MaterialType = materialType;
    //         }
    //     }
    // }


}
