using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Nano Ability")]
public class NanoAbility : Ability
{
    // Prefab for the nanotech effect
    public ParticleSystem particlePrefab;

    // Duration in seconds before the spawned particle effect is destroyed
    public float effectDuration = 2f;

    public override void Activate(GameObject user)
    {
        if (particlePrefab == null || user == null)
            return;

        // Instantiate the particle effect at the user's position and rotation
        ParticleSystem instance = Object.Instantiate(particlePrefab, user.transform.position, Quaternion.identity);
        instance.Play();

        // Destroy the particle effect after the set duration
        Object.Destroy(instance.gameObject, effectDuration);
    }
}
