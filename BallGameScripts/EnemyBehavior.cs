using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody enemyRb;

    private GameObject player;

    private PlayerController playerController;

    private Vector3 lookDirection;

    private GameObject enemySpawner;

    private SpawnManager spawnManager;

    [SerializeField]
    private float enemySpeed;


    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner");
        spawnManager = enemySpawner.GetComponent<SpawnManager>();
        enemyRb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if(playerController.isGameOver == false)
        {
            lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * enemySpeed * Time.deltaTime, ForceMode.Impulse);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
        {
            Destroy(gameObject);
            spawnManager.enemyCount--;
        }
    }
}
