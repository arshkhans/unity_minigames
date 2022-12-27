using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.Collections;

namespace Multiplayer
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private TMP_Text displayNameText;
        [SerializeField] private Renderer playerBody3D;
        private NetworkVariable<FixedString32Bytes> displayName = new NetworkVariable<FixedString32Bytes>();

        public override void OnNetworkSpawn()
        {
            // This is gonna fk me some day
            if (IsClient && !IsHost){
                displayNameText.text = displayName.Value.ToString();
            }else{
                SyncNames(OwnerClientId);
            }
        }

        private void SyncNames(ulong _clientId)
        {
            PlayerData? playerData = InitiateMultiplayer.GetPlayerData(_clientId);
            if (playerData.HasValue)
            {
                displayNameText.text = playerData.Value.PlayerName;
                displayName.Value = playerData.Value.PlayerName;
            }
        }

    }
}