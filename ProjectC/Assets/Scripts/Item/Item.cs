public class Item
{
    public ItemType Type { get; private set; }
    public string Name { get; private set; }
    public int BaseCost { get; private set; }
    public int BasePower { get; private set; }
    public ItemRarity Rarity { get; private set; }

    public Item(ItemType type, string name, int baseCost, int basePower, ItemRarity rarity)
    {
        Type = type;
        Name = name;
        BaseCost = baseCost;
        BasePower = basePower;
        Rarity = rarity;
    }

}
