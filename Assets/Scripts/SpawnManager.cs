using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerupPrefab;
    public GameObject boostPrefab;
    private float spawnPosX;
    private float spawnPosZ;
    private float xMin = -60;
    private float xMax = 15;
    private float zMin = -15;
    private float zMax = 15;
    private int enemyCount;
    public int waveNumber = 1;
    private bool isGameOver = false;

    void Start()
    {
        Vector3 powerupPosition = GenerateSpawnPosition() + new Vector3(0, 7, 0);
        Instantiate(powerupPrefab, powerupPosition, powerupPrefab.transform.rotation);

        //Instantiate(boostPrefab, GenerateSpawnPosition(), boostPrefab.transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && !isGameOver)
        {
            Vector3 powerupPosition = GenerateSpawnPosition() + new Vector3(0, 7, 0);
            Instantiate(powerupPrefab, powerupPosition, powerupPrefab.transform.rotation);
            //Instantiate(boostPrefab, GenerateSpawnPosition(), boostPrefab.transform.rotation);
            SpawnEnemyWave(++waveNumber * waveNumber);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        if (Random.Range(0, 1.0f) > 0.5)
        {
            xMin = -70;
            xMax = 40;
            zMin = -15;
            zMax = 50;
        }
        else
        {
            xMin = -45;
            xMax = 15;
            zMin = -35;
            zMax = 70;
        }

        spawnPosX = Random.Range(xMin, xMax);
        spawnPosZ = Random.Range(zMin, zMax);
        Vector3 spawnPosition = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPosition;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPosition() + new Vector3(0,5.5f,0), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].SetActive(false);
        }
    }
}
