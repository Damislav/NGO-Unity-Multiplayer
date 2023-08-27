using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAudio : MonoBehaviour
{
    private PlayerMovement player;
    private float footstepTimer;
    private float footstepTimerMax = .3f;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;

            if (player.isMoving)
            {

                AudioManager.Instance.PlayFootstepsSound(player.transform.position);
            }

        }
    }
}
