using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public GameObject playerProjectileAudioSource;
    [SerializeField] public Animator projectileAnimator;
    private float timer;

    private void Start()
    {
        playerProjectileAudioSource = GameObject.Find("PlayerProjectileAudioSource");
    }
    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > 3f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EnemyShip") && collider.GetComponent<Enemy>().isActive)
        {
            moveSpeed = 0;
            projectileAnimator.Play("ProjectileExplosion");
            playerProjectileAudioSource.GetComponent<AudioSource>().Play();
            GameObject spawner = GameObject.Find("Spawner");
            if (spawner.GetComponent<Spawner>().activeEnemies > 0)
            {
                spawner.GetComponent<Spawner>().activeEnemies--;
            }
            Destroy(collider.gameObject);
        }
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
