using NUnit.Framework;

public class ItemTests
{
    [Test]
    public void Constructor_Set()
    {
        var type = ItemType.Weapon;
        var name = "TestWeapon";
        var cost = 100;
        var power = 25;
        var rarity = ItemRarity.Rare;

        Item testItem = new Item(type, name, cost, power, rarity);

        Assert.AreEqual(type, testItem.Type);
        Assert.AreEqual(name, testItem.Name);
        Assert.AreEqual(cost, testItem.BaseCost);
        Assert.AreEqual(power, testItem.BasePower);
        Assert.AreEqual(rarity, testItem.Rarity);
    }
}