using UnityEngine;

/// <summary>
/// Provides dynamic difficulty scaling based on the number of active players.
/// Adjusts enemy health, damage and count to keep challenges balanced for solo and co-op play.
/// </summary>
public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    [Tooltip("Multiplier applied to enemy health for each additional player")]
    public float healthMultiplierPerAdditionalPlayer = 0.5f;

    [Tooltip("Multiplier applied to enemy damage for each additional player")]
    public float damageMultiplierPerAdditionalPlayer = 0.5f;

    [Tooltip("Multiplier applied to the number of enemies for each additional player")]
    public float countMultiplierPerAdditionalPlayer = 0.3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Optional: keep this manager persistent across scenes
        // DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Returns a scaled health value based on the number of active players.
    /// </summary>
    public int GetScaledHealth(int baseHealth)
    {
        int players = GetPlayerCount();
        float multiplier = 1f + (players - 1) * healthMultiplierPerAdditionalPlayer;
        return Mathf.CeilToInt(baseHealth * multiplier);
    }

    /// <summary>
    /// Returns a scaled damage value based on the number of active players.
    /// </summary>
    public int GetScaledDamage(int baseDamage)
    {
        int players = GetPlayerCount();
        float multiplier = 1f + (players - 1) * damageMultiplierPerAdditionalPlayer;
        return Mathf.CeilToInt(baseDamage * multiplier);
    }

    /// <summary>
    /// Returns a scaled count value based on the number of active players.
    /// </summary>
    public int GetScaledCount(int baseCount)
    {
        int players = GetPlayerCount();
        float multiplier = 1f + (players - 1) * countMultiplierPerAdditionalPlayer;
        return Mathf.CeilToInt(baseCount * multiplier);
    }

    // Helper method to determine number of player controllers currently active in the scene
    private int GetPlayerCount()
    {
        return FindObjectsOfType<PlayerController>().Length;
    }
}
