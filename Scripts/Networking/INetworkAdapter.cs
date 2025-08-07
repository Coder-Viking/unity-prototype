using UnityEngine;

namespace Networking
{
    /// <summary>
    /// Interface used to abstract away the underlying networking implementation.
    /// Implementations can wrap Mirror, Netcode for GameObjects or any other
    /// networking solution.  This allows the rest of the codebase to remain
    /// agnostic of the concrete networking library.
    /// </summary>
    public interface INetworkAdapter
    {
        /// <summary>
        /// True if the local instance is acting as server/host.
        /// </summary>
        bool IsServer { get; }

        /// <summary>
        /// True if the local instance is acting as a client.
        /// </summary>
        bool IsClient { get; }

        /// <summary>
        /// Start hosting a game session. Typically starts both server and client locally.
        /// </summary>
        void StartHost();

        /// <summary>
        /// Start connecting as a client to an existing host.
        /// </summary>
        void StartClient();

        /// <summary>
        /// Spawn a networked prefab at the specified position and rotation.  The
        /// prefab must be registered ahead of time using RegisterPrefab on the
        /// network manager/adapter.
        /// </summary>
        /// <param name="prefab">The prefab to spawn.</param>
        /// <param name="position">World position for the spawned instance.</param>
        /// <param name="rotation">World rotation for the spawned instance.</param>
        void Spawn(GameObject prefab, Vector3 position, Quaternion rotation);

        /// <summary>
        /// Register a prefab so it can be spawned over the network.  Some
        /// networking libraries require registration before spawning.
        /// </summary>
        /// <param name="prefab">The prefab to register.</param>
        void RegisterPrefab(GameObject prefab);
    }
}
