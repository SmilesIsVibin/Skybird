using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    [SerializeField] private int shieldLevel;
    [SerializeField] private int baseDuration;
    [SerializeField] private float timer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("shieldLevel"))
        {
            shieldLevel = PlayerPrefs.GetInt("shieldLevel");
            baseDuration += shieldLevel;
        }
        else
        {
            PlayerPrefs.SetInt("shieldLevel", 1);
            shieldLevel = 1;
            baseDuration += shieldLevel;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= baseDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag("Hostile"))
        {
            GameObject sfxSource = GameObject.Find("EnemyProjectileAudioSource");
            sfxSource.GetComponent<AudioSource>().Play();
            Destroy(target.gameObject);
        }
    }
}
