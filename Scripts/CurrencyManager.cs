using UnityEngine;

/// <summary>
/// Manages player currency and experience points.
/// Credits can be used to purchase items and upgrades.
/// Experience accumulates over time and can be used for leveling up.
/// </summary>
public class CurrencyManager : MonoBehaviour
{
    // Total amount of credits the player has
    public int credits { get; private set; }

    // Total amount of experience the player has
    public int experience { get; private set; }

    /// <summary>
    /// Adds credits to the player's balance.
    /// </summary>
    /// <param name="amount">The amount of credits to add.</param>
    public void AddCredits(int amount)
    {
        credits += Mathf.Max(0, amount);
    }

    /// <summary>
    /// Spends credits if available.
    /// </summary>
    /// <param name="amount">The cost to spend.</param>
    /// <returns>True if the transaction was successful.</returns>
    public bool SpendCredits(int amount)
    {
        if (credits >= amount)
        {
            credits -= amount;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds experience points to the player.
    /// </summary>
    /// <param name="amount">The amount of experience to add.</param>
    public void AddExperience(int amount)
    {
        experience += Mathf.Max(0, amount);
        // TODO: Consider level-up logic in a separate system
    }
}
