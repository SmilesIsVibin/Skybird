using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Enemy Projectile Info")]
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private Transform projectilePos;
    [SerializeField] private Player player;
    [SerializeField] private float timer;
    [SerializeField] private float spacing;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip playerShootSFX;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (player.isAlive)
        {
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if (timer > spacing)
                {
                    timer = 0;
                    Shoot();
                }
            }else if (Input.GetMouseButtonUp(0))
            {
                timer = 0;
            }

        }
    }

    private void Shoot()
    {
        playerAudioSource.pitch = Random.Range(0.95f, 1.05f);
        playerAudioSource.PlayOneShot(playerShootSFX);
        Instantiate(playerProjectile, projectilePos.position, Quaternion.identity);
    }
}
