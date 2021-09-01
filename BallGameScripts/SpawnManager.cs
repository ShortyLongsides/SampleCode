using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject forceMultiplierPrefab;

    [SerializeField]
    private float spawnTime = 5.0f;

    [SerializeField]
    private float spawnDelay = 5.0f;

    private GameObject player;

    private int levelNum;

    private PlayerController playerController;

    private float spawnPosX = 0f;

    private float spawnPosZ = 0f;

    public int enemyCount;

   
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        levelNum = 1;
        while(playerController.isGameOver == false)
        {
            Debug.Log("Level " + levelNum); //level start
            yield return new WaitForSecondsRealtime(5);

            StartCoroutine(SpawnPowerup());
            for(int i = 0; i < levelNum + 1; i++)
            {
                Instantiate(enemyPrefab, RandomSpawn(), enemyPrefab.transform.rotation);
                enemyCount++;
                yield return new WaitForSecondsRealtime(Random.Range(3,6));
                
            }
            yield return new WaitUntil(() => enemyCount == 0);
            levelNum++;
        }
        Debug.Log("Game Over!");
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSecondsRealtime(Random.Range(5, 15));
        Instantiate(forceMultiplierPrefab, RandomSpawn(), forceMultiplierPrefab.transform.rotation);
    }

    Vector3 RandomSpawn()
    {
        spawnPosX = Random.Range(-9, 9);
        spawnPosZ = Random.Range(-9, 9);
        Vector3 spawnLocation = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnLocation;
    }
}
