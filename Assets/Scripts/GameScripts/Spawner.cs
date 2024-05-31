using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Info")]
    [SerializeField] public List<string> objectPrefabs;
    [SerializeField] public string chosenEvent;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float eventTimer;
    [SerializeField] private float minsToEvent;
    [SerializeField] public float currentSpawnTime;
    [SerializeField] private float baseSpawnTime;
    [SerializeField] public bool isActive;
    [Header("Meteor Info")]
    [SerializeField] public GameObject meteorPrefab;
    [SerializeField] public float meteorMinSpawnTime;
    [SerializeField] public float meteorMaxSpawnTime;
    [SerializeField] private bool canSpeedUpMeteor;
    [SerializeField] public float meteorSpeedIncrease;
    [SerializeField] private float meteorTimer;
    [SerializeField] public float timeToMins;
    [SerializeField] public float maxIncrease;
    [SerializeField] private int skipPlace = 5;
    [Header("Enemy Info")]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject laserPrefab;
    [SerializeField] public int activeEnemies = 0;
    [SerializeField] public float minXPos;
    [SerializeField] public float maxXPos;
    [Header("Danger Alert Info")]
    [SerializeField] public GameObject alertPrefab;
    [Header("Item Info")]
    [SerializeField] private GameObject coinsPrefab;
    [SerializeField] private GameObject gemsPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float currencySpawnTimer;
    [SerializeField] private float currencyTime;
    [SerializeField] private float chanceForGems;
    [SerializeField] private float chanceForShield;

    private void Start()
    {
        chosenEvent = objectPrefabs[0];
        canSpeedUpMeteor = true;
        meteorSpeedIncrease = 0;
        meteorMaxSpawnTime = baseSpawnTime;
    }

    private void Update()
    {
        if (isActive)
        {
            if (spawnTimer > currentSpawnTime)
            {
                SelectObjectsToSpawn();
                spawnTimer = 0;
                eventTimer += 5;
                if (eventTimer >= minsToEvent)
                {
                    chosenEvent = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
                    Debug.Log(chosenEvent);
                    eventTimer = 0;
                }
            }
            spawnTimer += Time.deltaTime;

            if (canSpeedUpMeteor)
            {
                meteorTimer += Time.deltaTime;
                if (meteorTimer >= timeToMins)
                {
                    if(meteorSpeedIncrease <= maxIncrease && meteorMaxSpawnTime > meteorMinSpawnTime)
                    {
                        meteorSpeedIncrease += 0.025f;
                        meteorMaxSpawnTime -= 0.025f;
                        meteorTimer = 0;
                    }
                    else
                    {
                        canSpeedUpMeteor = false;
                    }
                }
            }

            if(currencySpawnTimer > currencyTime)
            {
                currencySpawnTimer = 0;
                SpawnCurrency();
            }
            currencySpawnTimer += Time.deltaTime;
        }
        
    }

    private void SelectObjectsToSpawn()
    {
        switch (chosenEvent)
        {
            case "METEOR": case "meteor": case "Meteor":
                currentSpawnTime = meteorMaxSpawnTime;
                SpawnMeteor();
                break;
                case "DANGER": case "danger": case "Danger":
                currentSpawnTime = baseSpawnTime;
                SpawnAlerts();
                break;
            case "ENEMY": case "enemy": case "Enemy":
                currentSpawnTime = baseSpawnTime;
                SpawnEnemy();
                break;
            default:
                SpawnMeteor();
                break;
        }
    }

    private void SpawnMeteor()
    {
        float xPos = -2.25f;
        int randNo;
        bool skipSpawn = false;
        for (int i =0; i < 5; i++)
        {
            randNo = Random.Range(1, 100);
            if (randNo <= 50 && skipSpawn == false && i != skipPlace)
            {
                skipSpawn = true;
                skipPlace = i;
                xPos += 1.125f;
                Debug.Log("Skipped Object " + skipPlace);
            }
            else if (i == 4 && skipSpawn == false)
            {
                skipSpawn = true;
                skipPlace = i;
                xPos += 1.125f;
                Debug.Log("Skipped");
            } else
            {
                GameObject newSpawnedObject = Instantiate(meteorPrefab);
                newSpawnedObject.GetComponent<PrefabMovement>().moveSpeed += meteorSpeedIncrease;
                newSpawnedObject.transform.position = new Vector2(xPos, transform.position.y + Random.Range(-0.5f, 0.5f));
                newSpawnedObject.transform.Rotate(0, 0, Random.Range(0, 360));
                xPos += 1.125f;
                Debug.Log("Spawned");
            }
        }
    }

    private void SpawnAlerts()
    {
        float xPos = -2.25f;
        int randNo;
        bool skipSpawn = false;
        for (int j = 0; j < 5; j++)
        {
            randNo = Random.Range(1, 100);
            if (randNo <= 50 && skipSpawn == false && j != skipPlace)
            {
                skipSpawn = true;
                skipPlace = j;
                xPos += 1.125f;
                Debug.Log("Skipped Object " + skipPlace);
            }
            else if (j == 4 && skipSpawn == false)
            {
                skipSpawn = true;
                skipPlace = j;
                xPos += 1.125f;
                Debug.Log("Skipped");
            }
            else
            {
                GameObject newSpawnedObject = Instantiate(alertPrefab);
                newSpawnedObject.transform.position = new Vector2(xPos, 4.5f);
                xPos += 1.125f;
                Debug.Log("Spawned");
            }
        }
    }

    private void SpawnEnemy()
    {
        if (activeEnemies == 0)
        {
            GameObject leftEnemyObject = Instantiate(enemyPrefab);
            leftEnemyObject.transform.position = new Vector2(minXPos, transform.position.y);
            activeEnemies++;

            GameObject rightEnemyObject = Instantiate(enemyPrefab);
            rightEnemyObject.transform.position = new Vector2(maxXPos, transform.position.y);
            activeEnemies++;

            GameObject middleEnemyObject = Instantiate(enemyPrefab);
            middleEnemyObject.GetComponent<Enemy>().maxY += 1.125f;
            middleEnemyObject.transform.position = new Vector2(0, transform.position.y);
            activeEnemies++;
        }
        float laserRange = Random.Range(minXPos, maxXPos);
        GameObject newLaser = Instantiate(laserPrefab);
        newLaser.transform.position = new Vector2(laserRange, transform.position.y);
    }

    private void SpawnCurrency()
    {
        float randRange = Random.Range(1, 100);
        if(randRange <= chanceForGems)
        {
            float gemRange = Random.Range(minXPos, maxXPos);
            GameObject newGemObject = Instantiate(gemsPrefab);
            newGemObject.transform.position = new Vector2(gemRange, transform.position.y);
            Debug.Log("Gems Spawned");
        }
        else if(randRange > chanceForGems && randRange <= chanceForShield)
        {
            float shieldRange = Random.Range(minXPos, maxXPos);
            GameObject newShieldObject = Instantiate(shieldPrefab);
            newShieldObject.transform.position = new Vector2(shieldRange, transform.position.y);
            Debug.Log("Shield Spawned");
        }
        else
        {
            float xPos = -2.25f;
            for (int k = 0; k < 5; k++)
            {
                GameObject newCoinObject = Instantiate(coinsPrefab);
                newCoinObject.transform.position = new Vector2(xPos, transform.position.y);
                xPos += 1.125f;
                Debug.Log("Coins Spawned");
            }
        }
    }
}
