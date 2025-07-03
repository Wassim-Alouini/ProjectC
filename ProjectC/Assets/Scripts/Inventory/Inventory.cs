using System;
using System.Collections.Generic;

public class Inventory
{

    public event Action OnInventoryChanged;
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Inventory();
            }
            return _instance;
        }
    }
    public List<Item> Items { get; private set; }
    public int Capacity { get; private set; }

    public void SetCapacity(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentException("Capacity cannot be negative.");
        }
        Capacity = capacity;
    }
    public void AddItem(Item item)
    {
        if (Items.Count >= Capacity)
        {
            throw new InvalidOperationException("Inventory is full.");
        }
        Items.Add(item);
        NotifyChange();
    }
    public void RemoveItemAt(int index)
    {
        if (index < 0 || index >= Items.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }
        Items.RemoveAt(index);
        NotifyChange();
        
    }
    public void ClearInventory()
    {
        Items.Clear();
        NotifyChange();
    }

    private Inventory()
    {
        Items = new List<Item>();
        Capacity = 0;
    }

    public void SortInventory()
    {
        Items.Sort();
        NotifyChange();
    }

    private void NotifyChange()
    {
        OnInventoryChanged?.Invoke();
    }
    
    #if UNITY_EDITOR
    public static void ResetInstance()
    {
        _instance = null;
    }
    #endif







}
