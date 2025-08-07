using UnityEngine;

public class CameraTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform[] cameraPoints;
    public float checkInterval = 0.1f;
    private int currentIndex = 0;
    private Camera cam;
    private float timer;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cameraPoints != null && cameraPoints.Length > 0)
        {
            currentIndex = 0;
            transform.position = cameraPoints[0].position;
            transform.rotation = cameraPoints[0].rotation;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckPlayerInView();
        }
    }

    void CheckPlayerInView()
    {
        if (player == null || cameraPoints == null || cameraPoints.Length == 0) return;

        Vector3 viewportPos = cam.WorldToViewportPoint(player.position);
        bool outOfView = viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f;
        if (outOfView)
        {
            // choose the camera point nearest to player, but different from current
            int bestIndex = currentIndex;
            float bestDistance = float.MaxValue;
            for (int i = 0; i < cameraPoints.Length; i++)
            {
                if (i == currentIndex) continue;
                float dist = Vector3.Distance(player.position, cameraPoints[i].position);
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestIndex = i;
                }
            }

            if (bestIndex != currentIndex)
            {
                currentIndex = bestIndex;
                transform.position = cameraPoints[currentIndex].position;
                transform.rotation = cameraPoints[currentIndex].rotation;
            }
        }
    }
}
