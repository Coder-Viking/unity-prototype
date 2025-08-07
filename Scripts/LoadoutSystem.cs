using UnityEngine;

[System.Serializable]
public class AbilitySlot
{
    public Ability ability;
    public KeyCode activationKey;
}

public class LoadoutSystem : MonoBehaviour
{
    [System.Serializable]
    public class Loadout
    {
        public string name;
        public GameObject weaponPrefab;
        public AbilitySlot[] abilities;
    }

    public Loadout[] availableLoadouts;

    public Transform weaponMountPoint;
    private GameObject currentWeapon;
    private AbilitySlot[] currentAbilities;

    public void EquipLoadout(int index)
    {
        if (index < 0 || index >= availableLoadouts.Length) return;

        Loadout loadout = availableLoadouts[index];

        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (loadout.weaponPrefab != null && weaponMountPoint != null)
        {
            currentWeapon = Instantiate(loadout.weaponPrefab, weaponMountPoint.position, weaponMountPoint.rotation, weaponMountPoint);
        }

        currentAbilities = loadout.abilities;
    }

    void Update()
    {
        if (currentAbilities == null) return;

        foreach (var slot in currentAbilities)
        {
            if (slot.ability != null && Input.GetKeyDown(slot.activationKey))
            {
                slot.ability.Activate(gameObject);
            }
        }
    }
}
