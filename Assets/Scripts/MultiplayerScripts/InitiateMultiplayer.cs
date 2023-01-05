using Unity.Netcode;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multiplayer
{
    public class InitiateMultiplayer : MonoBehaviour
    {

        private static Dictionary<ulong, PlayerData> clientData;

        // Start is called before the first frame update
        void Start()
        {
            NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;

            switch (Buttons.GameMode)
            {
                case "Host":
                    clientData = new Dictionary<ulong, PlayerData>();
                    clientData[NetworkManager.Singleton.LocalClientId] = new PlayerData(Buttons.PlayerName);
                    NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
                    NetworkManager.Singleton.StartHost();
                    break;

                case "Client":
                    var payload = JsonUtility.ToJson(new ConnectionPayload()
                    {
                        password = "",
                        playerName = Buttons.PlayerName,
                    });

                    byte[] payloadBytes = Encoding.ASCII.GetBytes(payload);

                    // Set password ready to send to the server to validate
                    NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;
                    NetworkManager.Singleton.StartClient();
                    break;

                default:
                    break;
            }
        }

        public static PlayerData? GetPlayerData(ulong clientId)
        {
            if (clientData.TryGetValue(clientId, out PlayerData playerData))
            {
                return playerData;
            }
            return null;
        }

        private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            string payload = Encoding.ASCII.GetString(request.Payload);
            var connectionPayload = JsonUtility.FromJson<ConnectionPayload>(payload);

            response.Approved = true;

            response.Position = Vector3.zero;
            response.Rotation = Quaternion.identity;

            if (response.Approved)
            {
                response.CreatePlayerObject = true;
                response.PlayerPrefabHash = null;

                response.Pending = false;
                if (!clientData.TryGetValue(request.ClientNetworkId, out PlayerData playerData)) {
                    Debug.Log("we dont got data");
                    clientData[request.ClientNetworkId] = new PlayerData(connectionPayload.playerName);
                }
            }
        }
    }
}
