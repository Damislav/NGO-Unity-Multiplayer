using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;

public class ConnectionButtons : NetworkBehaviour
{

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

}

