using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy Wave config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float TimeBtwSpawns = 0.5f;
    [SerializeField] float SpawnRandom = 0.3f;
    [SerializeField] int NumOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWayPoints()
    {
        var WaveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            WaveWaypoints.Add(child);
        }

        return WaveWaypoints;
    }
    public float GetTimeBtwSpawns()
    {
        return TimeBtwSpawns;
    }

    public float GetSpawnRandom()
    {
        return SpawnRandom;
    }
    public int GetNumOfEnemies()
    {
        return NumOfEnemies;
    }
    public float GetmoveSpeed()
    {
        return moveSpeed;
    }
}
