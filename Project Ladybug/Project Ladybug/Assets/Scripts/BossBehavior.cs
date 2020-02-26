using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    private bool inFight;
    private float counter;
    public int hp;
    void Start()
    {
        transform.localPosition = startPosition;
        hp = 50;
        inFight = false;
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (inFight && counter > 2f)
        {
            for (int i = 0; i < 7; i++)
            {
                Shoot(Quaternion.Euler(0f, 0f, Random.Range(-50f, 50f)));
            }
            counter = 0f;
        }
    }

    public void Spawn()
    {
        print("Spawned Boss");
        transform.localPosition = endPosition;
        inFight = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!inFight || !other.CompareTag("Bullet")) return;
            hp--;
        if (other.GetComponent<Projectile>() != null)
        {
            other.transform.position = other.GetComponent<Projectile>().deadPos;
            other.tag = "WaitingToSpawn";
        }
        if (hp <= 0)
            SceneScript.WinState();
    }

    private void Shoot(Quaternion bulletDirection)
    {
        Projectile[] bullets = FindObjectsOfType<Projectile>();
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed < 0f)
            {
                bullets[i].transform.position = transform.position;
                bullets[i].transform.position = new Vector3(transform.position.x, transform.position.y, 1.8f);
                bullets[i].transform.rotation = bulletDirection;
                bullets[i].tag = "EnemyBullet";
                break;
            }
        }
    }
}
