using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Lifetime : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;
    [SerializeField] float currentTime;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
