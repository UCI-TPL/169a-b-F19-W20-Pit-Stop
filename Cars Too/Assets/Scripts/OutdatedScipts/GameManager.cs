using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // private List<Enemy> enemies;

    [SerializeField] private List<Enemy> enemyPrefabs;

    [SerializeField] private float spawnRate; // Per Seconds

    [SerializeField] private GameObject player;

    [SerializeField] private int LevelCounter;

    private float timer;

    private bool stopSpawning;

    private int index;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = spawnRate;
        stopSpawning = false;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopSpawning) {
            
            timer -= Time.deltaTime;

            if(timer <= 0) {
                
                //This variable will be the location or locations that enemies spawn at
                //List<Vector3> spawnLocations

                Vector3 temporarySpawn = new Vector3(0,0,0);

                Instantiate(enemyPrefabs[0], temporarySpawn, Quaternion.identity);

                timer = spawnRate;
                index++;

                if(index >= enemyPrefabs.Count) {
                    stopSpawning = true;
                }
            }
        }
    }

    
}
