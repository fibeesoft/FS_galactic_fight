using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerShooter : MonoBehaviour
{
    public GameObject enemyFollowerPrefab;
    GameObject[] enemyFollowerSpawnSpots;
    float spawnTime;
    float timer;

    void Start()
    {
        timer = 0;
        spawnTime = 2f;
        enemyFollowerSpawnSpots = GameObject.FindGameObjectsWithTag("EnemyFollowerSpawnPos");
        print("spawn nr: " + enemyFollowerSpawnSpots.Length);
    }

    void SpawnEnemyFollower(){
        int randomNumber = Random.Range(0, enemyFollowerSpawnSpots.Length);
        GameObject a = Instantiate(enemyFollowerPrefab, enemyFollowerSpawnSpots[randomNumber].transform.position, Quaternion.identity);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime){
            SpawnEnemyFollower();
            timer = 0;
        }
    }
}
