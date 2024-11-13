using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] Zombies;
    public GameObject[] Spawns;

    public float spawnInterval = 5f; // Time in seconds between spawns
    private float timer = 0f; 

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnZombie();
            timer = 0f; 
        }
    }

    void SpawnZombie()
    {
        int randomIndex = Random.Range(0, Zombies.Length);
        
        int randomSpawnIndex = Random.Range(0, Spawns.Length);

        GameObject zombieToSpawn = Zombies[randomIndex];
        GameObject spawn = Spawns[randomSpawnIndex];
        Vector3 spawnPosition = spawn.transform.position;

        GameObject spawnedZombie = Instantiate(zombieToSpawn, spawnPosition + new Vector3(0,0,1.5f), Quaternion.identity);
        NavMeshAgent navMeshAgent = spawnedZombie.AddComponent<NavMeshAgent>();
        ZombieBehaviour behaviour = spawnedZombie.AddComponent<ZombieBehaviour>();
        behaviour.player = player;
    }
}
