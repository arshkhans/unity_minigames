using Unity.Netcode;
using UnityEngine;

public class InitaiteMultiplayer : MonoBehaviour
{
    private NetworkManager netManager;
    // Start is called before the first frame update
    void Start()
    {
        netManager = GetComponentInParent<NetworkManager>();
        
        switch (Buttons.mode)
        {
            case "Host":
                netManager.StartHost();

                break;
            case "Client":
                netManager.StartClient();
                break;
            default:
                break;
        }
    }

}
