using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown = 1f;

    public abstract void Activate(GameObject user);
}
