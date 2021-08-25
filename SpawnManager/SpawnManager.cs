using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    private PlayerController playerControllerScript;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private Vector3 spawnPos;

    [SerializeField]
    private float startDelay;

    [SerializeField]
    private float repeatRate;

    private void Awake()
    {
       playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver.Equals(false))
        {
            Instantiate(obstaclePrefab, spawnPos, transform.rotation);
        }
    }
}
