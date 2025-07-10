using System;

public static class InventoryEvents
{
    public static event Action<Item> OnItemDropped;

    public static void RaiseItemDropped(Item item)
    {
        OnItemDropped.Invoke(item);
    }
}
