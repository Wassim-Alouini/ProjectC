// using NUnit.Framework;

// public class ItemTests
// {
//     [Test]
//     public void Constructor_Set()
//     {
//         var type = ItemType.Weapon;
//         var name = "TestWeapon";
//         var cost = 100;
//         var power = 25;
//         var rarity = ItemRarity.Rare;
//         var materialType = MaterialType.Steel;

//         Item testItem = new Item(type, name, cost, power, rarity, materialType);

//         Assert.AreEqual(type, testItem.Type);
//         Assert.AreEqual(name, testItem.Name);
//         Assert.AreEqual(cost, testItem.BaseCost);
//         Assert.AreEqual(power, testItem.BasePower);
//         Assert.AreEqual(rarity, testItem.Rarity);
//         Assert.AreEqual(materialType, testItem.MaterialType);
//         Assert.IsNull(testItem.ResourceType);

//         Item testItem2 = new Item(type, name, cost, power, rarity, null, ResourceType.Goblin_Heart);

//         Assert.AreEqual(type, testItem2.Type);
//         Assert.AreEqual(name, testItem2.Name);
//         Assert.AreEqual(cost, testItem2.BaseCost);
//         Assert.AreEqual(power, testItem2.BasePower);
//         Assert.AreEqual(rarity, testItem2.Rarity);
//         Assert.IsNull(testItem2.MaterialType);
//         Assert.AreEqual(ResourceType.Goblin_Heart, testItem2.ResourceType);
//     }
// }