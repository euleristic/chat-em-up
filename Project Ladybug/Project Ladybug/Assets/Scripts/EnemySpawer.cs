using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int firstWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        var CurrentWave = waveConfigs[firstWave];
        StartCoroutine(SpawnAllEnemWave(CurrentWave));
    }

    private IEnumerator SpawnAllEnemWave(WaveConfig waveConfig)
    {
        Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(waveConfig.GetTimeBtwSpawns());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
