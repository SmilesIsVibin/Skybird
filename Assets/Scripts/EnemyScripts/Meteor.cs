using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private bool isBurningMeteor;

    private void Start()
    {
        if (isBurningMeteor)
        {
            GameObject meteorAudioSource = GameObject.Find("BurningMeteorAudioSource");
            meteorAudioSource.GetComponent<AudioSource>().Play();
            meteorAudioSource.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<Player>().isAlive)
        {
            other.gameObject.GetComponent<Player>().shipCurrentHealth = 0;
            Debug.Log("Dead");
        }
    }
}
