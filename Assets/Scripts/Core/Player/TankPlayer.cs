using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Cinemachine;
using Unity.Collections;
using System;

public class TankPlayer : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [Header("Settings")]
    [SerializeField] private int cameraPriority = 15;

    public NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            UserData userData = HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);

            playerName.Value = userData.userName;
        }

        if (IsOwner)
        {
            virtualCamera.Priority = cameraPriority;
        }
    }

}
