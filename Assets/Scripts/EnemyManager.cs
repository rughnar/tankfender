using System.Collections;
using System.Collections.Generic;
using Tankfender;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int totalLevelEnemies = 10;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int maxEnemiesAtTheSameTime = 5;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeBetweenSpawns = 4f;
    private GameManager gameManager;
    private int currentEnemiesOnLevel = 0;
    private int quantityEnemiesDestroyed = 0;
    private float lastEnemySpawnTime = 0f;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        lastEnemySpawnTime = 0f;
        Spawn(1);
    }

    void FixedUpdate()
    {
        if (Time.fixedTime - lastEnemySpawnTime > timeBetweenSpawns)
        {
            Spawn(1);
            lastEnemySpawnTime = Time.fixedTime;
        }
    }

    void Spawn(int quantity)
    {
        if (quantityEnemiesDestroyed + currentEnemiesOnLevel < totalLevelEnemies && currentEnemiesOnLevel < maxEnemiesAtTheSameTime)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            currentEnemiesOnLevel++;
        }
    }

    public void EnemyTakenDown()
    {
        currentEnemiesOnLevel -= 1;
        quantityEnemiesDestroyed += 1;
        if (quantityEnemiesDestroyed == totalLevelEnemies)
        {
            gameManager.WinGame();
        }
    }
}
