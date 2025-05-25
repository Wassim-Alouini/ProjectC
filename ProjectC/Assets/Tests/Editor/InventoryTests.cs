using System;
using NUnit.Framework;

public class InventoryTests
{
    private Item _sampleItem;

    [SetUp]
    public void SetUp()
    {
        Inventory.ResetInstance();
        _sampleItem = new Item(ItemType.Weapon, "TestWeapon", 100, 25, ItemRarity.Rare);
    }
    [Test]
    public void Constructor()
    {
        Inventory inventory = Inventory.Instance;
        Assert.IsNotNull(inventory);
        Assert.AreEqual(0, inventory.Capacity);
        Assert.IsEmpty(inventory.Items);
    }
    [Test]
    public void SetCapacity_SetsCapacityCorrectly()
    {
        Inventory inventory = Inventory.Instance;
        int newCapacity = 10;
        inventory.SetCapacity(newCapacity);
        Assert.AreEqual(newCapacity, inventory.Capacity);
    }
    [Test]
    public void SetCapacity_NegativeCapacity_ThrowsException()
    {
        Inventory inventory = Inventory.Instance;
        Assert.Throws<ArgumentException>(() => inventory.SetCapacity(-1));
    }
    [Test]
    public void AddItem_AddsItemToInventory()
    {
        Inventory inventory = Inventory.Instance;
        inventory.SetCapacity(5);
        inventory.AddItem(_sampleItem);
        Assert.AreEqual(1, inventory.Items.Count);
        Assert.AreEqual(_sampleItem, inventory.Items[0]);
    }

    [Test]
    public void AddItem_ExceedsCapacity_ThrowsException()
    {
        Inventory inventory = Inventory.Instance;
        inventory.SetCapacity(1);
        inventory.AddItem(_sampleItem);
        Assert.Throws<InvalidOperationException>(() => inventory.AddItem(new Item(ItemType.Weapon, "Test2", 150, 30, ItemRarity.Common)));
    }

    [Test]
    public void RemoveItemAt_RemovesItemAtIndex()
    {
        Inventory inventory = Inventory.Instance;
        inventory.SetCapacity(5);
        inventory.AddItem(_sampleItem);
        Assert.AreEqual(1, inventory.Items.Count);
        
        inventory.RemoveItemAt(0);
        Assert.IsEmpty(inventory.Items);
    }

    [Test]
    public void RemoveItemAt_InvalidIndex_ThrowsException()
    {
        Inventory inventory = Inventory.Instance;
        inventory.SetCapacity(5);
        inventory.AddItem(_sampleItem);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => inventory.RemoveItemAt(1));
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Inventory.ResetInstance();
    }
    
}