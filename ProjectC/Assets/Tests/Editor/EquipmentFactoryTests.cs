using NUnit.Framework;

using System.Collections.Generic;

public class EquipmentFactoryTests
{
    [Test]
    public void CraftEquipmentTest()
    {
        var handle = new Component(ComponentType.Handle, new List<ComponentTag>() { ComponentTag.Durable, ComponentTag.Magical }, "Mana Handle");
        var guard = new Component(ComponentType.Guard, new List<ComponentTag>() { ComponentTag.Light, ComponentTag.Magical }, "Mana Guard");
        var blade = new Component(ComponentType.Blade, new List<ComponentTag>() { ComponentTag.Blunt, ComponentTag.Brutal }, "Stone Blade");

        var expected = new Equipment(new List<ComponentTag>() { ComponentTag.Durable, ComponentTag.Magical, ComponentTag.Light, ComponentTag.Blunt, ComponentTag.Brutal }, EquipmentType.Sword, "Sword of testing");

        var actual = EquipmentFactory.CreateEquipmentItem(new List<Component>() { handle, guard, blade }, EquipmentType.Sword, "Sword of testing");

        Assert.AreEqual(expected.Type, actual.Type);
        CollectionAssert.AreEquivalent(expected.Tags, actual.Tags);
        Assert.AreEqual(expected.Name, actual.Name);
    }
} 