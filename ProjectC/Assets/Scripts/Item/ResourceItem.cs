public class ResourceItem : Item
{
    public ResourceType ResourceType { get; private set; }

    public ResourceItem(ResourceType type, string name, int cost, int power)
    {
        ResourceType = type;
        Name = name;
        BaseCost = cost;
        BasePower = power;
    }
}