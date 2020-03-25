using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public AudioClip dieSound;

    [SerializeField] float speed;
    [SerializeField] GameObject Bullet;
    [SerializeField] private int hp;
    [SerializeField] private float shootProbability;
    public Vector3 deadPos;
    public int enemyType;
    public Vector2 spawnRange;
    private Vector3 startPos;


    public static AudioSource audioSource;

    private void PlayDeathSound()
    {
        if(audioSource != null && dieSound != null)
        {
            audioSource.PlayOneShot(dieSound, 1f);
        }
    }

    void Start()
    {
        deadPos = transform.position;
    }
    void Update()
    {
        if (!CompareTag("WaitingToSpawn"))
        {
            switch (enemyType)
            {
                case 0:
                    transform.position += new Vector3(0f, Time.deltaTime * -speed, 0f);
                    break;
                case 1:
                    transform.position += new Vector3(Mathf.Sin(Time.time * 2) * 0.02f, Time.deltaTime * -speed, 0f);
                    if (Random.Range(0f, 1f) < shootProbability) Shoot();
                    break;
                default:
                    transform.position += new Vector3(0f, Time.deltaTime * -speed, 0f);
                    if (Random.Range(0f, 1f) < shootProbability) Shoot();
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            hp -= other.GetComponent<Projectile>().damage;
            if (hp <= 0)
            {
                transform.position = deadPos;
                transform.tag = "WaitingToSpawn";
                PlayDeathSound();
            }
            if (other.GetComponent<Projectile>().currentWeapon != PlayerMod.Weapon.Boomerang && other.GetComponent<Projectile>().currentWeapon != PlayerMod.Weapon.Orb)
            {
                other.transform.position = other.GetComponent<Projectile>().deadPos;
                other.tag = "WaitingToSpawn";
            }
        }
        else if (other.transform.CompareTag("Floor"))
            transform.position = new Vector3(Random.Range(-8f, -8f + spawnRange.x), Random.Range(10f, 10f + spawnRange.y), 1.8f);
        else if (other.transform.CompareTag("AliveEnemy"))
        {
            if (transform.position.y > other.transform.position.y)
                transform.position += new Vector3(0f, 1.5f, 0f);
        }
    }

    private void Shoot()
    {
        Projectile[] bullets = FindObjectsOfType<Projectile>();
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed < 0f)
            {
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = transform.rotation;
                bullets[i].tag = "EnemyBullet";
                break;
            }
        }
    }
}
