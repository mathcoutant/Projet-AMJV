using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private GameObject[] enemyTypes;

    private GameObject[] spawnPoints;

    private int enemyNumber = 0;
    public int maxEnemyNumber = 500;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        enemyTypes = new GameObject[] { enemy1, enemy2, enemy3 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // return true if the enemy is spawned
    public bool SpawnEnemy()
    {
        if (enemyNumber >= maxEnemyNumber) { return false; };
        Vector3 spawnPosition = selectSpawnPosition();
        GameObject newEnemy = Instantiate(selectEnemy(), spawnPosition, Quaternion.identity);
        enemyNumber++;
        return true;
    }

    private Vector3 selectSpawnPosition()
    {
        GameObject selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 currentPoint = selectedSpawnPoint.transform.position;
        currentPoint.y += .3f;
        return currentPoint;
    }
    private GameObject selectEnemy()
    {
        GameObject selectedEnemy = enemyTypes[Random.Range(0, enemyTypes.Length)];
        return selectedEnemy;
    }

    public void decreaseEnemyCounter()
    {
        enemyNumber -= 1;
    }
}
