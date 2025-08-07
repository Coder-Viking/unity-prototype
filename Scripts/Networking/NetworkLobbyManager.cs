using UnityEngine;
using Mirror;

namespace Networking
{
    /// <summary>
    /// Custom lobby manager derived from Mirror's NetworkRoomManager. Handles lobby logic
    /// such as reacting when all players are ready, players leaving, and spawning room
    /// and gameplay players. Configure RoomScene and GameplayScene in the inspector.
    /// </summary>
    public class NetworkLobbyManager : NetworkRoomManager
    {
        /// <summary>
        /// Called on the server when all players in the lobby are marked ready.
        /// We simply proceed to load the gameplay scene. Extend to add countdowns or other logic.
        /// </summary>
        public override void OnRoomServerPlayersReady()
        {
            base.OnRoomServerPlayersReady();
            // Start the game by switching scenes
            ServerChangeScene(GameplayScene);
        }

        /// <summary>
        /// Called when a player leaves the room. You can override this to handle cleanup.
        /// </summary>
        public override void OnRoomServerPlayerLeave(NetworkConnectionToClient conn)
        {
            base.OnRoomServerPlayerLeave(conn);
            Debug.Log($"Player {conn.connectionId} left the room");
        }

        /// <summary>
        /// Override to customise creation of the room player prefab. Here you can assign
        /// custom data or set up ready indicators.
        /// </summary>
        public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
        {
            return base.OnRoomServerCreateRoomPlayer(conn);
        }

        /// <summary>
        /// Called when adding a player to the gameplay scene. Override to transfer data
        /// from the room player to the gameplay player (e.g. chosen character or loadout).
        /// </summary>
        public override void OnRoomServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnRoomServerAddPlayer(conn);
            // Additional customization can be inserted here
        }
    }
}
