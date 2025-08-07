using UnityEngine;
using Mirror;

/// <summary>
/// Networked variant of the PlayerController using Mirror's NetworkBehaviour. This controller
/// runs input only on the local player and synchronizes movement and rotation across the network
/// via Commands executed on the server. Other clients receive updates automatically.
/// </summary>
public class NetworkPlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float backwardSpeedMultiplier = 0.5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float strafeSpeedMultiplier = 0.7f;

    [Header("Targeting")]
    [SyncVar] private bool isLockedOn = false;
    [SyncVar] private Transform currentTarget;

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (isLockedOn && currentTarget != null)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            CmdHandleStrafe(h, v);
            CmdRotateTowardsTarget();
        }
        else
        {
            float move = 0f;
            if (Input.GetKey(KeyCode.W)) move = 1f;
            else if (Input.GetKey(KeyCode.S)) move = -backwardSpeedMultiplier;

            float rotationInput = 0f;
            if (Input.GetKey(KeyCode.A)) rotationInput -= 1f;
            if (Input.GetKey(KeyCode.D)) rotationInput += 1f;

            CmdMove(move);
            CmdRotate(rotationInput);
        }
    }

    /// <summary>
    /// Command to move forward/backward on the server.
    /// </summary>
    /// <param name="move">Positive moves forward, negative moves backward.</param>
    [Command]
    private void CmdMove(float move)
    {
        Vector3 movement = transform.forward * move;
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Command to rotate the player on the server.
    /// </summary>
    /// <param name="rotationInput">-1 for left, 1 for right.</param>
    [Command]
    private void CmdRotate(float rotationInput)
    {
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Command for strafe movement when locked on to a target.
    /// </summary>
    [Command]
    private void CmdHandleStrafe(float horizontal, float vertical)
    {
        Vector3 direction = (transform.right * horizontal + transform.forward * vertical).normalized;
        transform.position += direction * moveSpeed * strafeSpeedMultiplier * Time.deltaTime;
    }

    /// <summary>
    /// Command to rotate towards the current target on the server.
    /// </summary>
    [Command]
    private void CmdRotateTowardsTarget()
    {
        if (currentTarget == null) return;
        Vector3 lookDirection = currentTarget.position - transform.position;
        lookDirection.y = 0f;
        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
