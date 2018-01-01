using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject enemy;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    int spawnCount = 10;
    public LevelManager levelHandler;
    public static int aliveCounter = 0;
    bool levelCompleted = false;
    public int LevelNumber;

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        
    }

    void Update()
    {
        if (aliveCounter == 0 && levelCompleted == true)
        {
            Debug.Log("Load Next Level");
            if (levelHandler.nextLevel < 4)
            {
                levelHandler.loadNextLevel();
            }
            else
            {
                SceneManager.LoadScene("YouWin");
            }
            
        }
    }


    void Spawn()
    {
        aliveCounter++;
        // If the player has no health left...
        if (playerHealth.currentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        if (--spawnCount == 0)
        {
            levelCompleted = true;
            Debug.Log("Level Complete: True");
            CancelInvoke("Spawn");
        }
    }
}


