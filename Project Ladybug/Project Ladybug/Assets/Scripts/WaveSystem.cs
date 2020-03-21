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
    [SerializeField] private int enemyCountWave4;
    [SerializeField] private int needToKill4;
    [SerializeField] private Vector2 spawnRange4;
    [SerializeField] private int enemyCountWave6;
    [SerializeField] private int needToKill6;
    [SerializeField] private Vector2 spawnRange6;
    [SerializeField] private int currentWave;
    [SerializeField] private Sprite dishSprite;
    [SerializeField] private Sprite forkSprite;
    [SerializeField] private Sprite knifeSprite;
    [SerializeField] private Sprite stdEnemyBullet;
    [SerializeField] private Sprite EnemyCalamari;
    [SerializeField] private Sprite EnemyUmbrella;
    [SerializeField] private Sprite waterDropSprite;
    [SerializeField] private Sprite EnemyRay;
    [SerializeField] private Sprite EnemySyringe;
    [SerializeField] private PlayerMod playerMod;
    private GameObject[] enemiesFound;
    private int enemyCount;
    private Projectile[] bullets;
    private Vector3 spawnPos;
    private Quaternion nullQuaternion;
    private bool answer;

    [SerializeField] private AudioClip bossmusic;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nullQuaternion = new Quaternion(0, 0, 0, 0);
        SpawnWave();
    }

    void Update()
    {
        //enemiesFound = GameObject.FindGameObjectsWithTag("AliveEnemy");
        enemyCount = 0;
        foreach (var enemy in enemiesFound)
            if (enemy.CompareTag("AliveEnemy"))
                enemyCount++;
        switch (currentWave)
        {
            case 0:
                if (!handler.visible && enemyCount == enemyCountWave0 - needToKill0)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 2:
                if (!handler.visible && enemyCount == enemyCountWave2 - needToKill2)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 4:
                if (!handler.visible && enemyCount == enemyCountWave4 - needToKill4)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 6:
                if (!handler.visible && enemyCount == enemyCountWave6 - needToKill6)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.GetComponent<EnemyBehavior>().deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
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
                handler.GetMessage("Mom", "Pick up brother");
                handler.visible = true;
                break;

            case 4:
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

                        if (answer)
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyCalamari;
                        else
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyUmbrella;
                        }
                        count++;
                    }
                    if (count == enemyCountWave4)
                        break;
                }
                if (!answer)
                {
                    bullets = FindObjectsOfType<Projectile>();
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].speed < 0f)
                        {
                            bullets[i].GetComponent<SpriteRenderer>().sprite = waterDropSprite;
                        }
                    }
                }
                break;

            case 5:

                handler.GetMessage("Mom", "Take the dog to the vet");
                handler.visible = true;
                break;

            case 6:
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
                        if (answer)
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyRay;
                        else
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemySyringe;
                        }
                        count++;
                    }
                    if (count == enemyCountWave6)
                        break;
                }
                if (!answer)
                {
                   /* bullets = FindObjectsOfType<Projectile>();
                    for (int i = 0; i < bullets.Length; i++)
                    {
                        if (bullets[i].speed < 0f)
                        {
                            bullets[i].GetComponent<SpriteRenderer>().sprite = ;
                        }
                    }*/
                }
                break;

            case 7:

                handler.GetMessage("Mom", "Attend your Uncle's funurel");
                handler.visible = true;
                break;

            case 8:
                Boss.Spawn();

                //hack
                if (bossmusic != null & audioSource != null)

                {
                    audioSource.clip = bossmusic;
                    audioSource.Play();
                }

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
        enemiesFound = GameObject.FindGameObjectsWithTag("AliveEnemy");
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

        switch (currentWave)
        {
            case 1:
                break;
            case 3:
                break;
            case 5:
                break;
            case 7:
                break;
            default:
                break;
        }

        currentWave++;
        bullets = FindObjectsOfType<Projectile>();
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].speed < 0f)
            {
                bullets[i].GetComponent<SpriteRenderer>().sprite = stdEnemyBullet;
            }
        }
        SpawnWave();
    }
}
