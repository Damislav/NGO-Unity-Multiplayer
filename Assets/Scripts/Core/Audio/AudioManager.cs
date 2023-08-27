using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }

    private void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position);
    }

    public void PlayFootstepsSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.tracks, position);
    }
    public void PlayBarrelShotSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.barrelShot, position);
    }
    public void PlayPickupSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.pickupSound, position);
    }
    public void PlayDamageTankSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.damageTank, position);
    }

}
