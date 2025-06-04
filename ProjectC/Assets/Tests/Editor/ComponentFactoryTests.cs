// using NUnit.Framework;

// using System.Collections.Generic;

// public class ComponentFactoryTests
// {
//     [Test]
//     public void CraftComponentTest()
//     {
//         var expected = new Component(ComponentType.Blade, new List<ComponentTag>(), "Steel Blade");
//         expected.addTag(ComponentTag.Durable);
//         var actual = ComponentFactory.CreateComponent(ComponentType.Blade, new Item(ItemType.Material, "Steel", 100, 25, ItemRarity.Rare, MaterialType.Steel));
//         Assert.AreEqual(expected.Type, actual.Type);
//         CollectionAssert.AreEquivalent(expected.Tags, actual.Tags);
//         Assert.AreEqual(expected.Name, actual.Name);
//     }
// }