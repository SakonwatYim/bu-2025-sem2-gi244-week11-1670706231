using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject[] powerUpPrefabs;
    public Wave[] waves;
    private int currentWave = 0;
    private Transform[] waveSpawnPoints;
    private Coroutine spawnCoroutine;
    // private Coroutine byeRoutine;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        //byeRoutine = StartCoroutine(Bye());
        StartCoroutine(WaveManager());
    }



    IEnumerator WaveManager()
    {
        while (currentWave < waves.Length)
        {
            Debug.Log("Starting Wave: " + currentWave);

            // เลือกจุดเกิดแบบสุ่ม
            SelectWaveSpawnPoints(currentWave);

            // power ups ขึ้นตอนเริ่ม
            for (int i = 0; i < waves[currentWave].numberOfPowerUp; i++)
            {
                RandomPowerUp();
            }

            spawnCoroutine = StartCoroutine(SpawnRoutine(currentWave));
            yield return spawnCoroutine;

            currentWave++;
        }
        Debug.Log("All waves complete");
    }

    void SelectWaveSpawnPoints(int waveIndex)
    {
        int count = waves[waveIndex].numberOfRandomSpawnPoint;
        waveSpawnPoints = new Transform[count];

        // Create list of all available spawn points
        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        // Randomly select 'count' spawn points
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            // Debug.Log("Selected spawn point: " + availableSpawnPoints[randomIndex].name);
            waveSpawnPoints[i] = availableSpawnPoints[randomIndex];
            availableSpawnPoints.RemoveAt(randomIndex);
        }
    }

    void RandomSpawn(int waveIndex)
    {
        if (waveSpawnPoints == null || waveSpawnPoints.Length == 0) return;
        
        var spawnPoint = waveSpawnPoints[Random.Range(0, waveSpawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    void RandomPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

        Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine(int waveIndex)
    {
        yield return new WaitForSeconds(waves[waveIndex].delayStart);

        for (int i = 0; i < waves[waveIndex].totalSpawnEnemies; i++)
        {
            RandomSpawn(waveIndex);
            yield return new WaitForSeconds(waves[waveIndex].spawnInterval);
        }
    }

    

    private void Update()
    {
        //if (Time.time > 3)
        //{
        //    //StopAllCoroutines();
        //    StopCoroutine(byeRoutine);
        //}
    }

// void RandomSpawn()
    // {
    //     var index = Random.Range(0, spawnPoints.Length);    
    //     var spawnPoint = spawnPoints[index];
    //     Instantiate(enemyPrefab, spawnPoint.position,Quaternion.identity);

    // }

    // IEnumerator Bye()
    // {
    //     while (true)
    //     {
    //         Debug.Log("Bye" + Time.frameCount + " " + Time.time);
    //         //yield return null;
    //         yield return new WaitForSeconds(1f);

    //         yield return Hello(4);
    //         yield return new WaitForSeconds(1F);
            
    //         if(Time.time > 5)
    //         {
    //             yield break;
    //         }
    //     }
    // }

    // IEnumerator Hello(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     Debug.Log("Hello" + Time.frameCount);
    //     Debug.Log("Hello" + Time.frameCount);
    //     Debug.Log("Hello" + Time.frameCount);
    //     yield return null;
    //     Debug.Log("Hello" + Time.frameCount);
    //     yield return null;
    //     yield return null;
    //     Debug.Log("Hello" + Time.frameCount);
    // }
}
