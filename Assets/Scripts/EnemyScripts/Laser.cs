using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>().isAlive)
        {
            other.GetComponent<Player>().shipCurrentHealth = 0;
            Debug.Log("Dead");
        }
    }
}
