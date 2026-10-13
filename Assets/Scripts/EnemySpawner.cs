using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO[] waveConfigs;
    [SerializeField] float timeBetheenWaves = 1f;
    [SerializeField] bool isLooping ;
    WaveConfigSO currentWave;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for(int i = 0; i<currentWave.GetEnemyCount();i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform);

                    yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
                }
                //wave ends
                yield return new WaitForSeconds(timeBetheenWaves);

                
            }   
        } while (isLooping);
        
        
        
        
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
