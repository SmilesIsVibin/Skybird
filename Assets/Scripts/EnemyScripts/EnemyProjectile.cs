using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D projectileRB;
    public float force;
    private float lifeTimer;
    [SerializeField] public Animator projectileAnimator;
    [SerializeField] public GameObject enemyProjectileAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyProjectileAudioSource = GameObject.Find("EnemyProjectileAudioSource");
        Vector3 direction = player.transform.position - transform.position;
        projectileRB.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 270);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer > 3f)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            projectileRB.velocity = new Vector2(transform.position.x, transform.position.y) * 0;
            enemyProjectileAudioSource.GetComponent<AudioSource>().Play();
            projectileAnimator.Play("EnemyProjectileExplosion");
            other.GetComponent<Player>().shipCurrentHealth -= 1;
        }
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
