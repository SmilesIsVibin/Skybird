using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCreation : MonoBehaviour
{
    [SerializeField] private GameObject shieldEffect;
    private void OnTriggerEnter2D (Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            GameObject pickupAudioSource = GameObject.Find("PickupAudioSource");
            pickupAudioSource.GetComponent<AudioSource>().Play();
            GameObject newShield = Instantiate(shieldEffect, new Vector3 (target.gameObject.transform.position.x, target.gameObject.transform.position.y, 0), Quaternion.identity);
            newShield.transform.parent = target.gameObject.transform;
            Destroy(this.gameObject);
        }
    }
}
