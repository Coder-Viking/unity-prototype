using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple loot system that provides random item drops from a list of possible items.
/// Can be used by enemies or chests to generate rewards for the player.
/// </summary>
public class LootSystem : MonoBehaviour
{
    [Tooltip("List of items that can be dropped by this loot system")]
    public List<Item> possibleLoot;

    [Tooltip("Chance that any loot will drop (0 to 1)")]
    [Range(0f, 1f)]
    public float dropChance = 0.5f;

    /// <summary>
    /// Returns a random item from the possible loot list based on the drop chance.
    /// Returns null if no item should be dropped.
    /// </summary>
    public Item GetRandomLoot()
    {
        if (possibleLoot == null || possibleLoot.Count == 0)
        {
            return null;
        }
        // Determine if an item should drop
        if (Random.value > dropChance)
        {
            return null;
        }
        int index = Random.Range(0, possibleLoot.Count);
        return possibleLoot[index];
    }

    /// <summary>
    /// Attempts to drop loot at a given world position. If an item is returned, instantiate
    /// a pickup or add it to the player's inventory. This method is a placeholder and
    /// should be expanded with actual pickup logic.
    /// </summary>
    public void DropLoot(Vector3 position)
    {
        Item loot = GetRandomLoot();
        if (loot != null)
        {
            // TODO: Instantiate item pickup prefab here or add to player's inventory directly
            Debug.Log($"Dropped loot: {loot.itemName}");
        }
    }
}
