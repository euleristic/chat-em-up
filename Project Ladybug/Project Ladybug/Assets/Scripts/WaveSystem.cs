using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private BossBehavior Boss;
    [SerializeField] private TextMessageHandler handler;
    [SerializeField] private int enemyCountWave0;
    [SerializeField] private int needToKill0;
    [SerializeField] private Vector2 spawnRange0;
    [SerializeField] private int enemyCountWave2;
    [SerializeField] private int needToKill2;
    [SerializeField] private Vector2 spawnRange2;
    [SerializeField] private int currentWave;
    [SerializeField] private Sprite dishSprite;
    [SerializeField] private Sprite forkSprite;
    [SerializeField] private Sprite stdEnemyBullet;
    private GameObject[] enemiesFound;
    private Projectile[] bullets;
    private Vector3 spawnPos;
    private Quaternion nullQuaternion;
    private bool answer;
    void Start()
    {
        nullQuaternion = new Quaternion(0, 0, 0, 0);
        SpawnWave();
    }

    void Update()
    {
        enemiesFound = GameObject.FindGameObjectsWithTag("AliveEnemy");

        if (!handler.visible && currentWave == 0 && enemiesFound.Length == enemyCountWave0 - needToKill0)
        {
            foreach (var enemy in enemiesFound)
            {
                enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                enemy.transform.tag = "WaitingToSpawn";
            }
            currentWave++;
            SpawnWave();
        }
        else if (!handler.visible && currentWave == 2 && enemiesFound.Length == enemyCountWave2 - needToKill2)
        {
            foreach (var enemy in enemiesFound)
            {
                enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                enemy.transform.tag = "WaitingToSpawn";
            }
            currentWave++;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        switch (currentWave)
        {
            case 0:
                EnemyBehavior[] enemies = FindObjectsOfType<EnemyBehavior>();
                int count = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].transform.CompareTag("WaitingToSpawn") && enemies[i].enemyType == 0)
                    {
                        spawnPos = new Vector3(Random.Range(-8f, -8f + spawnRange0.x), Random.Range(10f, 10f + spawnRange0.y), 1.8f);
                        enemies[i].transform.tag = "AliveEnemy";
                        enemies[i].GetComponent<EnemyBehavior>().spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        count++;
                    }
                    if (count == enemyCountWave0)
                        break;
                }
                break;
            case 1:
                handler.GetMessage("Mom", "Do the dishes");
                handler.visible = true;
                break;
            case 2:
                enemies = FindObjectsOfType<EnemyBehavior>();
                count = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].transform.CompareTag("WaitingToSpawn") && enemies[i].enemyType == 1)
                    {
                        spawnPos = new Vector3(Random.Range(-8f, -8f + spawnRange0.x), Random.Range(10f, 10f + spawnRange0.y), 1.8f);
                        enemies[i].transform.tag = "AliveEnemy";
                        enemies[i].GetComponent<EnemyBehavior>().spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        if (!answer)
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = dishSprite;
                        }
                        count++;
                    }
                    if (count == enemyCountWave2)
                        break;
                }
                if (!answer)
                {
                    bullets = FindObjectsOfType<Projectile>();
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].speed < 0f)
                        {
                            bullets[i].GetComponent<SpriteRenderer>().sprite = forkSprite;
                        }
                    }
                }
                break;
            case 3:
                Boss.Spawn();
                bullets = FindObjectsOfType<Projectile>();
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].speed < 0f)
                    {
                        bullets[i].GetComponent<SpriteRenderer>().sprite = stdEnemyBullet;
                    }
                }
                break;
            default:
                break;
        }

    }

    public void AnswerHit(string answer_in)
    {
        if (!handler.visible) return;
        if (answer_in == "Yes")
            answer = true;
        else if (answer_in == "No")
            answer = false;
        else
            Debug.Log("Error: wrong");
        handler.HideMessage();
        handler.visible = false;
        currentWave++;
        SpawnWave();
    }
}
