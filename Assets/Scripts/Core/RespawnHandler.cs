using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;



public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private NetworkObject playerPrefab;


    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }
        TankPlayer.OnPlayerSpawned += HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned += HandleDespawnedSpawned;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) { return; }

        TankPlayer[] players = FindObjectsOfType<TankPlayer>();
        foreach (TankPlayer player in players)
        {
            HandlePlayerSpawned(player);
        }
        TankPlayer.OnPlayerSpawned -= HandlePlayerSpawned;
        TankPlayer.OnPlayerDespawned -= HandleDespawnedSpawned;
    }

    private void HandleDespawnedSpawned(TankPlayer player)
    {
        player.Health.OnDie += (health) => HandlePlayerDied(player);

    }

    private void HandlePlayerSpawned(TankPlayer player)
    {
        player.Health.OnDie -= (health) => HandlePlayerDied(player);
    }

    private void HandlePlayerDied(TankPlayer player)
    {
        Destroy(player.gameObject);

        StartCoroutine(RespawnPlayer(player.OwnerClientId));


    }
    private IEnumerator RespawnPlayer(ulong ownerClientId)
    {
        yield return null;
        NetworkObject playerInstance = Instantiate(playerPrefab, SpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        playerInstance.SpawnAsPlayerObject(ownerClientId);
    }
}
