using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class ProjectileLauncher : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject serverProjectilePrefab;
    [SerializeField] private GameObject clientProjectilePrefab;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Collider2D playerCollider;



    [Header("Settings")]

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float muzzleFlashDuration;


    private bool shouldFire;
    private float previousFireTime;
    private float muzzleFlashTimer;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }


    private void Update()
    {
        if (muzzleFlashTimer > 0)
        {
            muzzleFlashTimer -= Time.deltaTime;

            if (muzzleFlashTimer <= 0f)
            {
                muzzleFlash.SetActive(false);

            }
        }
        if (!IsOwner) { return; }
        if (!shouldFire) { return; }

        if (Time.time < (1 / fireRate) + previousFireTime) { return; }

        PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.up);
        SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);
        muzzleFlashTimer = muzzleFlashDuration;

        previousFireTime = Time.time;

    }


    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        muzzleFlash.SetActive(true);
        muzzleFlashTimer = muzzleFlashDuration;
        GameObject projectileInstance = Instantiate(
            clientProjectilePrefab,
             spawnPos,
             Quaternion.identity);

        projectileInstance.transform.up = direction;

        SpawnDummyProjectileClientRpc(spawnPos, direction);

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        //if projectile has rb it will be stored inside of rb param
        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * projectileSpeed;
        }

    }

    // so that any other client can see
    [ClientRpc]
    private void SpawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if (IsOwner) { return; }

        PrimaryFireServerRpc(spawnPos, direction);
    }


    // we are trusting client that he says where he is
    [ServerRpc]
    private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(
           serverProjectilePrefab,
            spawnPos,
            Quaternion.identity);

        projectileInstance.transform.up = direction;

        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());

        //if projectile has rb it will be stored inside of rb param
        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.forward * projectileSpeed;
        }
    }
    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }



}
