using UnityEngine;
using Mirror;

namespace Networking
{
    /// <summary>
    /// Concrete implementation of <see cref="INetworkAdapter"/> for the Mirror
    /// networking library. Attach this component alongside a NetworkManager to
    /// wrap Mirror operations in a framework-agnostic API.
    /// </summary>
    [RequireComponent(typeof(NetworkManager))]
    public class MirrorNetworkAdapter : MonoBehaviour, INetworkAdapter
    {
        private NetworkManager _manager;

        private void Awake()
        {
            _manager = GetComponent<NetworkManager>();
        }

        public bool IsServer => NetworkServer.active;

        public bool IsClient => NetworkClient.active;

        public void StartHost()
        {
            if (_manager != null && !_manager.isNetworkActive)
            {
                _manager.StartHost();
            }
        }

        public void StartClient()
        {
            if (_manager != null && !_manager.isNetworkActive)
            {
                _manager.StartClient();
            }
        }

        public void Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (_manager == null || prefab == null)
                return;
            // Instantiate locally and spawn on the server
            GameObject obj = Instantiate(prefab, position, rotation);
            if (NetworkServer.active)
            {
                NetworkServer.Spawn(obj);
            }
            else
            {
                Debug.LogWarning("Spawn called on client without server authority");
            }
        }

        public void RegisterPrefab(GameObject prefab)
        {
            if (_manager == null || prefab == null)
                return;
            if (!_manager.spawnPrefabs.Contains(prefab))
            {
                _manager.spawnPrefabs.Add(prefab);
            }
        }
    }
}
