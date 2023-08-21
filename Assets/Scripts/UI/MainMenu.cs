using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private TMP_InputField joinCodeFailed;



    public async void StartHost()
    {
        await HostSingleton.Instance.GameManager.StartHostAsync();
    }
    public async void StartClient()
    {
        await ClientSingleton.Instance.GameManager.StartClientAsync(joinCodeFailed.text);
    }

}
