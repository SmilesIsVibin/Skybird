using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] public float deathTimer;
    private float timer;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        if(timer > deathTimer)
        {
            Destroy(this.gameObject);
        }
    }
}
