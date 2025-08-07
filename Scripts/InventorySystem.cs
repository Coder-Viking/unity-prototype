using UnityEngine;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    public int maxSlots = 20;
    public List<Item> items = new List<Item>();

    public bool AddItem(Item item)
    {
        if (items.Count >= maxSlots) return false;
        items.Add(item);
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
}
