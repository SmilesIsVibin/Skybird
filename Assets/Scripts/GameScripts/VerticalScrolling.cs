using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrolling : MonoBehaviour
{
    public GameObject backgroundPrefab1;
    public GameObject backgroundPrefab2;
    public float scrollSpeed = 1.0f;
    public bool backgroundScrolling;

    private GameObject currentBackground;
    private int backgroundNo;
    private GameObject nextBackground;
    private Transform spawnPoint;

    void Start()
    {
        backgroundScrolling = true;
        spawnPoint = transform;
        SpawnInitialBackground();
    }

    void Update()
    {
        if (backgroundScrolling)
        {
            float step = scrollSpeed * Time.deltaTime;
            currentBackground.transform.position += Vector3.down * step;
            nextBackground.transform.position += Vector3.down * step;

            // Check if the current background is out of view
            if (currentBackground.transform.position.y < -10.05f)
            {
                if (backgroundNo == 1)
                {
                    backgroundNo = 2;
                }
                else
                {
                    backgroundNo = 1;
                }
                Destroy(currentBackground);
                currentBackground = nextBackground;
                SpawnNextBackground();
            }
        }
    }

    void SpawnInitialBackground()
    {
        backgroundNo = 1;
        currentBackground = Instantiate(backgroundPrefab1, spawnPoint.position, Quaternion.identity);
        nextBackground = Instantiate(backgroundPrefab2, spawnPoint.position + Vector3.up * 10.05f, Quaternion.identity);
    }

    void SpawnNextBackground()
    {
        Vector3 spawnPosition = spawnPoint.position + Vector3.up * 10.05f;
        if (backgroundNo == 1)
        {
            nextBackground = Instantiate(backgroundPrefab2, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned 1st Bg");
        }
        else if (backgroundNo == 2)
        {
            nextBackground = Instantiate(backgroundPrefab1, spawnPosition, Quaternion.identity);
            Debug.Log("Spawned 2nd Bg");
        }
        else
        {
            nextBackground = Instantiate(backgroundPrefab1, spawnPosition, Quaternion.identity);
            Debug.Log("Bug Occured");
        }
    }
}

