using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnTimer;
    public float spawnTime;
    public int enemyNumberUpBound = 10;
    public static int enemyNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (enemyNumberUpBound > enemyNumber && spawnTimer > spawnTime)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }
    void SpawnEnemy()
    {
        GameObject.Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemyNumber++;
        //print("Spawn Enemy");
    }
}
