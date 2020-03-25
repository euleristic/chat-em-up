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

    [SerializeField] float boomerangDecrementer;
    public float burstExponentiation;
    void Start()
    {
        damage = 1;
        deadPos = transform.position;
        boomerangDecrementer = 0f;
        clock = 0.0f;
        enemies = FindObjectsOfType<EnemyBehavior>();
        burstExponentiation = 1f;
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
                    boomerangDecrementer -= boomerangAcceleration * Time.deltaTime;
                    if (speed + boomerangDecrementer > 0.5)
                    {
                        transform.position += (transform.up * speed + new Vector3(0f, boomerangDecrementer));
                        //transform.rotation = Quaternion.Euler(0, 0, boomerangRotationSpeed * Time.time);
                    }
                    else
                    {
                        transform.position += (transform.position - player.transform.position).normalized * (speed + boomerangDecrementer);
                    }
                    break;
                case PlayerMod.Weapon.Starburst:
                    transform.position += transform.up * Time.deltaTime * (speed + burstExponentiation);
                    burstExponentiation *= burstSpeedFactor;
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
        if (currentWeapon == PlayerMod.Weapon.Boomerang && other.CompareTag("Player"))
        {
            transform.position = deadPos;
            tag = "WaitingToSpawn";
            boomerangDecrementer = 0;
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
            burstExponentiation = burstSpeedFactor;
        }
        if (currentWeapon == PlayerMod.Weapon.Homing && !other.CompareTag("Player"))
        {
            enemyTarget = null;
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
