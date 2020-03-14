using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerupPrefab;
    private float spawnPosX;
    private float spawnPosZ;
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 1;

    void Start()
    {
        Vector3 powerupPosition = GenerateSpawnPosition() + new Vector3(0, 2, 0);
        Instantiate(powerupPrefab, powerupPosition, powerupPrefab.transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            Vector3 powerupPosition = GenerateSpawnPosition() + new Vector3(0, 2, 0);
            Instantiate(powerupPrefab, powerupPosition, powerupPrefab.transform.rotation);
            SpawnEnemyWave(++waveNumber);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        spawnPosX = Random.Range(-spawnRange, spawnRange);
        spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPosition;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length - 1);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }
}
