using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public PowerupEffect pickupCurrency;
    private GameObject pickupAudioSource;

    private void Start()
    {
        pickupAudioSource = GameObject.Find("PickupAudioSource");
    }

    private void OnTriggerEnter2D(Collider2D targetObject)
    {
        if (targetObject.CompareTag("Player"))
        {
            pickupAudioSource.GetComponent<AudioSource>().Play();
            pickupCurrency.ApplyEffect(targetObject.gameObject);
            Destroy(this.gameObject);
        }
    }
}
