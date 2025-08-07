using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float backwardSpeedMultiplier = 0.5f;
    public float rotationSpeed = 200f;
    public float strafeSpeedMultiplier = 0.7f;

    [Header("Targeting")]
    public bool isLockedOn = false;
    public Transform currentTarget;

    private Coroutine alignCoroutine;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (isLockedOn && currentTarget != null)
        {
            // Strafe movement: uses WASD relative to player orientation while locked on
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
            transform.position += direction * moveSpeed * strafeSpeedMultiplier * Time.deltaTime;

            // rotate to face target continuously
            Vector3 lookDirection = currentTarget.position - transform.position;
            lookDirection.y = 0f;
            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            float move = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                move = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                move = -backwardSpeedMultiplier;
            }

            Vector3 moveVector = transform.forward * move;
            transform.position += moveVector * moveSpeed * Time.deltaTime;

            float rotationInput = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                rotationInput -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotationInput += 1f;
            }

            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);

            // If locked on but currently not moving, start align coroutine
            if (isLockedOn && currentTarget != null && rotationInput == 0f && move == 0f)
            {
                if (alignCoroutine == null)
                {
                    alignCoroutine = StartCoroutine(AlignToTargetAfterDelay(0.5f));
                }
            }
            else
            {
                if (alignCoroutine != null)
                {
                    StopCoroutine(alignCoroutine);
                    alignCoroutine = null;
                }
            }
        }
    }

    private IEnumerator AlignToTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isLockedOn && currentTarget != null)
        {
            Vector3 lookDir = currentTarget.position - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = targetRot;
            }
        }
        alignCoroutine = null;
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
        isLockedOn = target != null;
        if (!isLockedOn)
        {
            alignCoroutine = null;
        }
    }

    public void ClearTarget()
    {
        currentTarget = null;
        isLockedOn = false;
        if (alignCoroutine != null)
        {
            StopCoroutine(alignCoroutine);
            alignCoroutine = null;
        }
    }
}
