using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public Player playercharater;
    public GameObject enemy;
    public float spawntime = 10f;
    public Transform[] spawnPoints;

    void Start()
    {
        //reapeats spawning without timer 
        InvokeRepeating("Spawn", spawntime, spawntime);

    }

    // Update is called once per frame
    void Spawn()
    {
        if (playercharater.health <= 0)
        {
            return;

        }


        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        //creates enemys then spawn them 
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
