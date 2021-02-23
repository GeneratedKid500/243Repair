using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawning : MonoBehaviour
{
    public static EnemySpawning instance;

    public Transform[] spawnRadius;
    
    float maxY;
    float minY;
    float[] xSpawn;

    public GameObject[] enemy;

    public int maxAtOnce;
    public int maxNumberOfEnemies;

    public float timeBetweenSpawns = 0.5f;
    float spawnTime;

    int totalEnemies;
    public int currentEnemies;

    void Start()
    {
        instance = this;
        maxNumberOfEnemies = 2;
        maxY = spawnRadius[0].position.y;
        minY = spawnRadius[1].position.y;

        xSpawn = new float[2] { spawnRadius[0].position.x, spawnRadius[1].position.x };
    }

    void Update()
    {
        if (currentEnemies < maxAtOnce && totalEnemies < maxNumberOfEnemies && enemy.Length-1 <= 0 && spawnTime <= 0)
        {
            Instantiate<GameObject>(enemy[0], new Vector3(xSpawn[Random.Range(0, 2)], Random.Range(minY, maxY), 0), Quaternion.identity);
            spawnTime = timeBetweenSpawns;
            totalEnemies++;
            currentEnemies++;
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }

        if (totalEnemies == 60)
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void Unlocked2()
    {
        maxNumberOfEnemies = 15;
        timeBetweenSpawns -= 0.5f;
    }

    public void Unlocked3()
    {
        maxNumberOfEnemies = 60;
        timeBetweenSpawns -= 1.5f;
    }
}
