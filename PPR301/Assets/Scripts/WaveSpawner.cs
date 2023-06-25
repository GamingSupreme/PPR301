using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Variables")]
    //Reference to what were spawning
    public GameObject enemyObj;
    //amount of enemies thatll spawn
    public int enemiesPerWave = 1;
    //time between waves
    public float timeBetweenWave = 2f;

    //The actual wave were on
    private float waveNumber = 1;
    //Countdown before we start next wave
    private float countdownTimer = 0f;
    //Track enemies alive
    public int enemiesAlive = 0;



    void Start()
    {
        countdownTimer = timeBetweenWave;
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        //if time to next wave is 0 and no enemies are alive start spawning next wave
        if (countdownTimer <= 0f && enemiesAlive <= 0){
            //spawn wave and reset timer
            SpawnWave();
            countdownTimer = timeBetweenWave;
        }

        //decrease timer every frame
        countdownTimer -= Time.deltaTime;
    }

    private void SpawnWave(){
        //for the number of waves we need to spawn an additional enemy
        for (int i = 0; i < waveNumber; i++)
        {
            //spawn enemy at empty objects position with some randomness so everyone doesnt spawn ontop of each other
            Instantiate(enemyObj, transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0 , Random.Range(-5.0f, 5.0f)), transform.rotation);
            enemiesAlive++;
        }
        //increase wave number
        waveNumber++;
    }

    public void EnemyKilled(){
        enemiesAlive--;
    }


}
