using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer
{
    public class Player : NetworkBehaviour
    {
        // private NetworkVariable<PlayerData> PlayerPosition = new NetworkVariable<PlayerData>(new PlayerData{},
        //     NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
        
        // public struct PlayerData : INetworkSerializable
        // {
        //     public float horizontalInput;
        //     public float verticalInput;

        //     public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        //     {
        //         serializer.SerializeValue(ref horizontalInput);
        //         serializer.SerializeValue(ref verticalInput);
        //     }
        // }

        // Changed OnIsServerAuthoritative()

        [Header("Movement")]
        [SerializeField] private float moveSpeed;

        Rigidbody rb;

        public Text myText;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        Vector3 moveDirection;

        public override void OnNetworkSpawn()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        private void MyInput()
        {
            // PlayerPosition.Value = new PlayerData {
            //     horizontalInput = Input.GetAxisRaw("Horizontal"),
            //     verticalInput = Input.GetAxisRaw("Vertical")
            // };

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";
        }

        public void MovePlayer()
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        void Update()
        {
            if (!IsOwner) return;
            
            MyInput();
            MovePlayer();
        }
    }
}