using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float maxY;
    [SerializeField] public bool isActive;
    [Header("Enemy Projectile Info")]
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform projectilePos;
    [SerializeField] private float timer;
    [SerializeField] private float spacing;
    [SerializeField] private float noOfShots;
    [SerializeField] private float shootIntervals;
    [SerializeField] private float cooldown;
    [SerializeField] private bool canShoot = true;
    [Header("Follow Player")]
    [SerializeField] private float rotationModifier;
    [SerializeField] private float rotationSpeed;
    [Header("Enemy Audio")]
    [SerializeField] private GameObject enemyAudioSource;
    [SerializeField] private GameObject enemyShootingAudioSource;
    private GameObject gameManager;

    private void Start()
    {
        isActive = false;
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        enemyAudioSource = GameObject.Find("EnemyAudioSource");
        enemyShootingAudioSource = GameObject.Find("EnemyShootingAudioSource");
        enemyAudioSource.GetComponent<AudioSource>().Play();
    }

    public void ShootProjectile()
    {
        enemyShootingAudioSource.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        enemyShootingAudioSource.GetComponent<AudioSource>().Play();
        Instantiate(enemyProjectile, projectilePos.position, Quaternion.identity);
    }

    private void Update()
    {
        if(transform.position.y > maxY)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        if(transform.position.y <= maxY && canShoot)
        {
            isActive = true;
            timer += Time.deltaTime;
            if (timer > spacing)
            {
                timer = 0;
                ShootProjectile();
                noOfShots++;
                if (noOfShots >= shootIntervals)
                {
                    canShoot = false;
                    noOfShots = 0;
                    StartCoroutine("Cooldown");
                }
            }
        }
        if (gameManager.GetComponent<GameManager>().gameIsActive == false)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    private void FollowPlayer()
    {
        if(player.GetComponent<Player>().isAlive)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        }
    }
}
