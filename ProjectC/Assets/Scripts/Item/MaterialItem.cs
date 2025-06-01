public class MaterialItem : Item
{
    public MaterialType MaterialType { get; private set; }

    public MaterialItem(MaterialType type, string name, int cost, int power)
    {
        MaterialType = type;
        Name = name;
        BaseCost = cost;
        BasePower = power;
    }
}