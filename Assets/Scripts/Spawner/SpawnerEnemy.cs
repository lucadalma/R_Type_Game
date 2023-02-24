using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] Transform edge1;
    [SerializeField] Transform edge2;
    [SerializeField] float spawnEnemyRate;
    [SerializeField] float spawnGemRate;
    [SerializeField] public GameObject Boss;
    [SerializeField] public GameObject lifeGem;
    [SerializeField] public Transform bossPosition;
    GameManager gameManager;
    GameObject enemyToSpawn;
    float spawnTimer;
    float spawnGemTimer;
    float upLimit, downLimit;
    int numberBoss = 0;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();


        upLimit = edge1.position.y;
        downLimit = edge2.position.y;
    }

    void Update()
    {
        if (gameManager.wave == GameManager.Waves.Wave1)
        {
            enemyToSpawn = gameManager.enemyWave1;
        }
        else if (gameManager.wave == GameManager.Waves.Wave2)
        {
            spawnEnemyRate = 1f;
            enemyToSpawn = gameManager.enemyWave2;
        }
        else if (gameManager.wave == GameManager.Waves.Wave3)
        {
            spawnEnemyRate = 1.2f;
            enemyToSpawn = gameManager.enemyWave3;
        }
        else if (gameManager.wave == GameManager.Waves.Boss) 
        {
            spawnEnemyRate += Time.time;
            if (numberBoss == 0) 
            {
                SpawnBoss();
                numberBoss++;
            }
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnEnemyRate)
        {
            SpawnEnemys();
        }

        spawnGemTimer += Time.deltaTime;

        if (spawnGemTimer >= spawnGemRate) 
        {
            SpawnGem();
        }
    }

    private void SpawnEnemys() 
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(upLimit, downLimit));
        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        spawnTimer = 0;
    }

    private void SpawnBoss() 
    {
        Vector2 spawnPosition = new Vector2(bossPosition.position.x, bossPosition.position.y);
        GameObject boss = Instantiate(Boss, spawnPosition, Quaternion.identity);
    }

    private void SpawnGem()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(upLimit, downLimit));
        GameObject gem = Instantiate(lifeGem, spawnPosition, Quaternion.identity);
        spawnGemTimer = 0;
    }
}
