using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Hostile"))
        {
            Destroy(collider.gameObject);
        }
    }
}
