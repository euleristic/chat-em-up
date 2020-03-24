using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 deadPos;
    public bool shortFuse;
    public int damage;
    public float fuse;
    private float clock;
    public PlayerMod.Weapon currentWeapon;
    public GameObject player;
    private EnemyBehavior[] enemies;
    private EnemyBehavior enemyTarget;

    public float boomerangRange;
    public float boomerangRotationSpeed;
    public float boomerangAcceleration;

    public float burstAOEBase;
    public float burstAOEFactor;
    public float burstSpeedFactor;

    public float homingAngle;
    public float homingRotationSpeed;
    public float homingDistance;

    private float boomerangDecrementer;
    void Start()
    {
        damage = 1;
        deadPos = transform.position;
        boomerangDecrementer = boomerangRange;
        clock = 0.0f;
        enemies = FindObjectsOfType<EnemyBehavior>();
    }

    void FixedUpdate()
    {

        if (!CompareTag("WaitingToSpawn"))
        {
            if (shortFuse)
                clock += Time.deltaTime;
            switch (currentWeapon)
            {
                case PlayerMod.Weapon.Boomerang:
                    transform.position += (transform.up * speed + new Vector3(0f, boomerangDecrementer));
                    transform.localRotation = Quaternion.Euler(0, 0, boomerangRotationSpeed * Time.time);
                    boomerangDecrementer -= boomerangAcceleration;

                    if (speed + boomerangDecrementer < 0)
                    {
                        transform.position += transform.right * 1 / (transform.position.y - player.transform.position.y);
                    }
                    break;
                case PlayerMod.Weapon.Starburst:
                    transform.position += transform.up * speed * burstSpeedFactor;
                    burstSpeedFactor *= burstSpeedFactor;
                    break;
                case PlayerMod.Weapon.Homing:
                    if (enemyTarget == null)
                    {
                        foreach (var enemy in enemies)
                        {
                            if ((enemy.transform.position - transform.position).magnitude <= homingDistance && enemy.CompareTag("AliveEnemy"))
                            {
                                enemyTarget = enemy;
                                break;
                            }
                        }
                    }
                    else
                        transform.up = Vector3.Lerp(transform.up, enemyTarget.transform.position - transform.position, homingRotationSpeed);
                    transform.position += (transform.up * speed);
                    break;
                default:
                    transform.position += (transform.up * speed);
                    
                    break;
            }
            if (clock > fuse)
            {
                transform.position = deadPos;
                tag = "WaitingToSpawn";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentWeapon == PlayerMod.Weapon.Boomerang)
        {
            if (!other.transform.CompareTag("Player")) return;
            transform.position = deadPos;
            tag = "WaitingToSpawn";
        }
        if (currentWeapon == PlayerMod.Weapon.Starburst)
        {
            foreach (var enemy in enemies)
            {
                if ((enemy.transform.position - transform.position).magnitude <= burstAOEBase + burstSpeedFactor * burstSpeedFactor && enemy.CompareTag("AliveEnemy"))
                {
                    enemy.transform.position = deadPos;
                    enemy.transform.tag = "WaitingToSpawn";
                }
            }
        }
        if (currentWeapon == PlayerMod.Weapon.Homing)
        {
            enemyTarget = null;
            transform.position = deadPos;
            tag = "WaitingToSpawn";
        }

        if (other.transform.CompareTag("Yes") || other.transform.CompareTag("No"))
        {
            GameObject.FindObjectOfType<TextMessageHandler>().takeDamage(other.tag);
            transform.position = deadPos;
            tag = "WaitingToSpawn";
        }
        if (other.transform.CompareTag("Wall") && transform.CompareTag("Bullet"))
        {
            transform.position = deadPos;
            tag = "WaitingToSpawn";
        }
        if ((other.transform.CompareTag("Floor") || other.transform.CompareTag("Wall")) && transform.CompareTag("EnemyBullet"))
        {
            transform.position = deadPos;
            tag = "WaitingToSpawn";
        }
    }
}
