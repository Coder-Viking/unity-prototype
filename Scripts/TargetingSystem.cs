using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public float targetRange = 15f;
    public string enemyTag = "Enemy";
    public PlayerController playerController;

    private List<Transform> targets = new List<Transform>();
    private int currentIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AcquireTargets();
            CycleTarget();
        }
    }

    void AcquireTargets()
    {
        targets.Clear();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= targetRange)
            {
                targets.Add(enemy.transform);
            }
        }
        targets.Sort((a, b) => Vector3.Distance(transform.position, a.position).CompareTo(Vector3.Distance(transform.position, b.position)));
    }

    void CycleTarget()
    {
        if (targets.Count == 0)
        {
            currentIndex = -1;
            if (playerController != null)
                playerController.ClearTarget();
            return;
        }

        currentIndex = (currentIndex + 1) % targets.Count;
        if (playerController != null)
            playerController.SetTarget(targets[currentIndex]);
    }

    public void ClearTarget()
    {
        currentIndex = -1;
        if (playerController != null)
            playerController.ClearTarget();
    }
}
