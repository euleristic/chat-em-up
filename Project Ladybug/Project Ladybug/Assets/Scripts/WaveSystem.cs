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
    [SerializeField] private EnemyMod enemyMod;
    [SerializeField] private SpriteRenderer YesIcon, NoIcon;
    [SerializeField] private Sprite Apple, Twin, Triple, Big, ShortF, Wet, Rest, standardIcon, orbIcon, BoomerIcon, StarIcon, ArrowIcon, HomingIcon;
    [SerializeField] private float appleScale, twinScale, tripleScale, bigScale, shortFScale, wetScale, restScale, standarsScale, orbScale, boomerScale, starScale, arrowScale, homingScale;
    private EnemyBehavior[] enemiesFound;
    private int enemyCount;
    private Projectile[] bullets;
    private Vector3 spawnPos;
    private Quaternion nullQuaternion;
    private bool answer;
    private Vector3 unitScale;

    private EnemyBehavior[] enemyBehaviors;

    [Header("Sounds")]
    [SerializeField] private AudioClip OOF;
    [SerializeField] private AudioClip firstGuyDeathSound;
    [SerializeField] private AudioClip dishyDeathSound;
    [SerializeField] private AudioClip umbrellaDeathSound;
    [SerializeField] private AudioClip syringeDeathSound;

    [SerializeField] private AudioClip bossmusic;
    private AudioSource audioSource;

    void Start()
    {
        EnemyBehavior.audioSource = GetComponent<AudioSource>();
        unitScale = new Vector3(1f, 1f, 1f);
        audioSource = GetComponent<AudioSource>();
        nullQuaternion = new Quaternion(0, 0, 0, 0);
        SpawnWave();
    }

    private void SetEnemySound(AudioClip sound, EnemyBehavior eb)
    {
        AudioClip sfx;
        if (sound != null)
        {
            sfx = sound;
        }
        else 
            sfx = OOF;
        eb.dieSound = sfx;         
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
                if (!handler.visible && enemyCount <= enemyCountWave0 - needToKill0)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                        
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 2:
                if (!handler.visible && enemyCount <= enemyCountWave2 - needToKill2)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 4:
                if (!handler.visible && enemyCount <= enemyCountWave4 - needToKill4)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.deadPos;
                        enemy.transform.tag = "WaitingToSpawn";
                    }
                    currentWave++;
                    SpawnWave();
                }
                break;
            case 6:
                if (!handler.visible && enemyCount <= enemyCountWave6 - needToKill6)
                {
                    foreach (var enemy in enemiesFound)
                    {
                        enemy.transform.position = enemy.deadPos;
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
            case -1:
                handler.GetMessage("Mom", "Are you playing that stupid game again!?");
                YesIcon.sprite = Apple;
                NoIcon.sprite = orbIcon;
                YesIcon.transform.localScale = unitScale * appleScale;
                NoIcon.transform.localScale = unitScale * orbScale;
                handler.visible = true;
                break;            
            case 0:
                EnemyBehavior[] enemies = FindObjectsOfType<EnemyBehavior>();
                int count = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].transform.CompareTag("WaitingToSpawn") && enemies[i].enemyType == 0)
                    {
                        spawnPos = new Vector3(Random.Range(-8f, -8f + spawnRange0.x), Random.Range(10f, 10f + spawnRange0.y), 1.8f);
                        enemies[i].transform.tag = "AliveEnemy";
                        enemies[i].spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        enemies[i].speed = enemyMod.enemy0Speed;
                        enemies[i].hp = enemyMod.enemy0HP;
                        count++;
                        SetEnemySound(firstGuyDeathSound, enemies[i]);
                    }
                    if (count == enemyCountWave0)
                        break;
                }
                break;
            case 1:
                handler.GetMessage("Mom", "Do the dishes");
                YesIcon.sprite = BoomerIcon;
                NoIcon.sprite = Wet;
                YesIcon.transform.localScale = unitScale * boomerScale;
                NoIcon.transform.localScale = unitScale * wetScale;
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
                        enemies[i].spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        enemies[i].speed = enemyMod.Enemy1Speed;
                        enemies[i].shootProbability = enemyMod.Enemy1ShootProbability;
                        enemies[i].hp = enemyMod.Enemy1HP;

                        if (!answer)
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = dishSprite;
                            SetEnemySound(dishyDeathSound, enemies[i]);
                        }
                        else
                        {
                            SetEnemySound(firstGuyDeathSound, enemies[i]);
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
                YesIcon.sprite = Twin;
                NoIcon.sprite = HomingIcon;
                YesIcon.transform.localScale = unitScale * twinScale;
                NoIcon.transform.localScale = unitScale * homingScale;
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
                        enemies[i].spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        enemies[i].speed = enemyMod.Enemy2Speed;
                        enemies[i].shootProbability = enemyMod.Enemy2ShootProbability;
                        enemies[i].hp = enemyMod.Enemy2HP;

                        if (answer)
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyCalamari;
                            SetEnemySound(firstGuyDeathSound, enemies[i]);
                        }
                        else
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyUmbrella;
                            SetEnemySound(umbrellaDeathSound, enemies[i]);
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
                YesIcon.sprite = StarIcon;
                NoIcon.sprite = Triple;
                YesIcon.transform.localScale = unitScale * starScale;
                NoIcon.transform.localScale = unitScale * tripleScale;
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
                        enemies[i].spawnRange = spawnRange0;
                        enemies[i].transform.position = spawnPos;
                        enemies[i].speed = enemyMod.Enemy3Speed;
                        enemies[i].shootProbability = enemyMod.Enemy3ShootProbability;
                        enemies[i].hp = enemyMod.Enemy3HP;
                        if (answer)
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemyRay;
                            SetEnemySound(firstGuyDeathSound, enemies[i]);
                        }
                        else
                        {
                            enemies[i].GetComponent<SpriteRenderer>().sprite = EnemySyringe;
                            SetEnemySound(syringeDeathSound, enemies[i]);
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

                handler.GetMessage("Mom", "Attend your Uncle's funeral");
                YesIcon.sprite = Rest;
                NoIcon.sprite = ArrowIcon;
                YesIcon.transform.localScale = unitScale * restScale;
                NoIcon.transform.localScale = unitScale * arrowScale;
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
        enemiesFound = FindObjectsOfType<EnemyBehavior>();
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
            case -1:
                if (answer)
                    playerMod.healthy = true;
                else
                    playerMod.currentWeapon = PlayerMod.Weapon.Orb;
                break;
            case 1:
                if (answer)
                    playerMod.currentWeapon = PlayerMod.Weapon.Boomerang;
                else
                    playerMod.wet = true;
                break;
            case 3:
                if (answer)
                    playerMod.twinShot = true;
                else
                    playerMod.currentWeapon = PlayerMod.Weapon.Homing;
                break;
            case 5:
                if (answer)
                    playerMod.currentWeapon = PlayerMod.Weapon.Starburst;
                else
                    playerMod.tripleShot = true;
                break;
            case 7:
                if (answer)
                    playerMod.rest = true;
                else
                    playerMod.currentWeapon = PlayerMod.Weapon.Arrow;
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
