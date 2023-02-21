using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] Transform edge1;
    [SerializeField] Transform edge2;
    [SerializeField] int spawnRate;
    GameManager gameManager;
    GameObject enemyToSpawn;
    float spawnTimer;
    float upLimit, downLimit;

        void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        enemyToSpawn = gameManager.enemyLevel;
        upLimit = edge1.position.y;
        downLimit = edge2.position.y;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            SpawnEnemys();
        }
    }

    private void SpawnEnemys() 
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(upLimit, downLimit));
        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        spawnTimer = 0;
    }
}
