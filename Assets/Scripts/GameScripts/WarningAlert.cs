using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningAlert : MonoBehaviour
{
    [Header("Burning Meteor Info")]
    [SerializeField] public GameObject burningMeteorPrefab;

    private void Start()
    {
        GameObject warningAudioSource = GameObject.Find("WarningAudioSource");
        warningAudioSource.GetComponent<AudioSource>().Play();
    }

    public void SpawnBurningMeteor()
    {
        GameObject newBurningMeteor = Instantiate(burningMeteorPrefab);
        newBurningMeteor.transform.position = new Vector2(transform.position.x, 7f);
        Destroy(this.gameObject);
    }
}
