using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;

    // enemy prefabs
    public GameObject slimePrefab;
    public GameObject spiderPrefab;
    public GameObject beePrefab;
    public GameObject batPrefab;
    public GameObject ladybugPrefab;
    public GameObject snailPrefab; 

    public int currentEnemyCount = 0;
    public int maxEnemies = 15;
    public float spawnRate = 3f;

    private int platformIndex = 0;

    private string[] enemyTypes = new string[] { "Ground", "Sky", "Platform" };
    private string[] groundEnemies = new string[] { "Slime", "Spider" };
    private string[] skyEnemies = new string[] { "Bee", "Bat" };
    private string[] platformEnemies = new string[] { "Ladybug", "Snail" };

    // spawn points for each type of enemy
    private Vector3[] groundSpawnPts = new Vector3[] { new Vector3(8f, -3.34f, 24.85192f) };
    private Vector3[] skySpawnPts = 
        new Vector3[] { new Vector3(8.4f, -1f, 24.85192f), new Vector3(8f, 0.6f, 24.85192f), new Vector3(8f, 2.6f, 24.85192f), new Vector3(8f, 4f, 24.85192f)};
    private Vector3[] platformSpawnPts = 
        new Vector3[] { new Vector3(-6.2f, 0.185f, 24.85192f), new Vector3(6f, 0.185f, 24.85192f), new Vector3(1.542f, 3.65f, 24.85192f), new Vector3(-7.75f, 1.92f, 24.85192f)};

	
	void Start () {
        InvokeRepeating("Spawn", 2f, spawnRate);
	}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Spawn()
    {
        if (currentEnemyCount == maxEnemies)
        {
            return;
        }

        string randomEnemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];

        switch (randomEnemyType)
        {
            case "Ground":
                spawnGroundEnemy ();
                break;

            case "Sky":
                spawnSkyEnemy ();
                break;

            case "Platform":
                spawnPlatformEnemy ();
                break;

            default:
                break;
        }

        currentEnemyCount++;
    }

    private void spawnGroundEnemy()
    {
        Vector3 spawnPt = groundSpawnPts[Random.Range(0, groundSpawnPts.Length)];
        string randomGroundEnemy = groundEnemies[Random.Range(0, groundEnemies.Length)];

        switch (randomGroundEnemy)
        {
            case "Slime":
                Instantiate(slimePrefab, spawnPt, Quaternion.identity);
                break;

            case "Spider":
                Instantiate(spiderPrefab, spawnPt, Quaternion.identity);
                break;

            default:
                break;
                
        }
    }

    private void spawnSkyEnemy()
    {
        Vector3 spawnPt = skySpawnPts[Random.Range(0, skySpawnPts.Length)];
        string randomSkyEnemy = skyEnemies[Random.Range(0, skyEnemies.Length)];

        switch (randomSkyEnemy)
        {
            case "Bee":
                Instantiate(beePrefab, spawnPt, Quaternion.identity);
                break;

            case "Bat":
                Instantiate(batPrefab, spawnPt, Quaternion.identity);
                break;

            default:
                break;
        }
    }

    // platform enemies are spawned at each platform (in order) to avoid crowding platforms with multiple enemies
    private void spawnPlatformEnemy()
    {
        Vector3 spawnPt = platformSpawnPts[platformIndex];
        string randomPlatformEnemy = platformEnemies[Random.Range(0, platformEnemies.Length)];

        switch (randomPlatformEnemy)
        {
            case "Ladybug":
                Instantiate(ladybugPrefab, spawnPt, Quaternion.identity);
                break;

            case "Snail":
                Instantiate(snailPrefab, spawnPt, Quaternion.identity);
                break;

            default:
                break;
        }

        // if all platforms have enemies, loops back to first platform
        if (platformIndex == platformSpawnPts.Length)
        {
            platformIndex = 0;
        }

        else { platformIndex++; }
    }

}
