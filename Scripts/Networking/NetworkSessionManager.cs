using UnityEngine;
using Mirror;

namespace Networking
{
    /// <summary>
    /// High-level manager encapsulating the start and stop operations for a Mirror network session.
    /// Attach this component alongside a NetworkManager in your bootstrap scene to provide
    /// simple methods for hosting, joining and stopping sessions.
    /// </summary>
    [RequireComponent(typeof(NetworkManager))]
    public class NetworkSessionManager : MonoBehaviour
    {
        private NetworkManager networkManager;

        private void Awake()
        {
            networkManager = GetComponent<NetworkManager>();
        }

        /// <summary>
        /// Starts a host (server + client in one process) if no network is currently active.
        /// </summary>
        public void StartHost()
        {
            if (networkManager != null && !networkManager.isNetworkActive)
            {
                networkManager.StartHost();
            }
        }

        /// <summary>
        /// Connects as a client to the given address. If no address is specified, defaults to localhost.
        /// </summary>
        public void StartClient(string address = null)
        {
            if (networkManager != null && !networkManager.isNetworkActive)
            {
                if (!string.IsNullOrWhiteSpace(address))
                {
                    networkManager.networkAddress = address;
                }
                networkManager.StartClient();
            }
        }

        /// <summary>
        /// Stops the current session gracefully, whether running as host, server or client.
        /// </summary>
        public void StopSession()
        {
            if (networkManager == null)
                return;

            if (NetworkServer.active && NetworkClient.isConnected)
            {
                networkManager.StopHost();
            }
            else if (NetworkServer.active)
            {
                networkManager.StopServer();
            }
            else if (NetworkClient.isConnected)
            {
                networkManager.StopClient();
            }
        }
    }
}
