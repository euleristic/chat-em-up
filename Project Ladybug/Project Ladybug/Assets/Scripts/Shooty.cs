﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private AudioSource source;
    [SerializeField] private PlayerMod playerMod;
    [SerializeField] private Sprite standardBullet;
    [SerializeField] private Sprite orbBullet;
    [SerializeField] private Sprite boomerangBullet;
    [SerializeField] private Sprite starburstBullet;
    [SerializeField] private Sprite arrowBullet;
    [SerializeField] private Sprite homingBullet;

    private PlayerMod.Weapon currentWeapon;
    private bool bigShot, shortFuse;
    private bool boomerangShot; 
    Projectile[] bullets;
    public float shoodspeed;
    private float lastshot;
    private float orbClock;

    private bool arrowBurstShot;
    private int arrowsShot;
    private float arrowClock;
    void Start()
    {
        bullets = FindObjectsOfType<Projectile>();
        ChangeWeapon(playerMod.currentWeapon);
        source = GetComponent<AudioSource>();
        bigShot = shortFuse = false;
        boomerangShot = false;
        orbClock = 0.0f;
        arrowBurstShot = false;
        arrowClock = 0.0f;
    }
    
    void Update()
    {
        if (currentWeapon != playerMod.currentWeapon)
        {
            ChangeWeapon(playerMod.currentWeapon);
        }
        if (!bigShot && playerMod.bigShot)
        {
            MakeBigShot();
            bigShot = true;
        }
        if (!shortFuse && playerMod.bigShot)
        {
            MakeShortFuse();
            shortFuse = true;
        }

        if (currentWeapon != playerMod.currentWeapon)
        {
            ChangeWeapon(playerMod.currentWeapon);
        }
        if (Input.GetButtonDown("Jump") && Time.time > lastshot + shoodspeed && currentWeapon != PlayerMod.Weapon.Orb && currentWeapon != PlayerMod.Weapon.Arrow)
        {
            source.Play();
            if (playerMod.tripleShot)
            {
                FireBullet(transform.rotation);
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.tripleAngleOffSet));
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.tripleAngleOffSet));
            }
            else if (playerMod.twinShot)
            {
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.twinAngleOffSet / 2));
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.twinAngleOffSet / 2));
            }
            else
                FireBullet(transform.rotation);
            lastshot = Time.time;
        }
        else if (Input.GetButton("Jump") && Time.time > lastshot + shoodspeed && currentWeapon == PlayerMod.Weapon.Orb && orbClock <= playerMod.orbMaxChargeDuration)
            orbClock += Time.deltaTime * playerMod.orbChargeSpeed;


        if (currentWeapon == PlayerMod.Weapon.Arrow && Input.GetButtonDown("Jump") && (!arrowBurstShot) && Time.time > lastshot + shoodspeed)
        {
            arrowBurstShot = true;
            lastshot = Time.time;
        }

        if (arrowBurstShot)
        {
            arrowClock += Time.deltaTime;
            if (arrowClock >= playerMod.arrowDelta)
            {
                source.Play();
                if (playerMod.tripleShot)
                {
                    FireBullet(transform.rotation);
                    FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.tripleAngleOffSet));
                    FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.tripleAngleOffSet));
                }
                else if (playerMod.twinShot)
                {
                    FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.twinAngleOffSet / 2));
                    FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.twinAngleOffSet / 2));
                }
                else
                    FireBullet(transform.rotation);
                arrowClock = 0f;
                arrowsShot++;
            }
            if (arrowsShot >= playerMod.arrowBulletsPerBurst)
            {
                arrowsShot = 0;
                arrowBurstShot = false;
            }
        }

        if (currentWeapon == PlayerMod.Weapon.Orb && Input.GetButtonUp("Jump") && orbClock >= playerMod.orbMinChargeDuration)
        {
            source.Play();
            if (playerMod.tripleShot)
            {
                FireBullet(transform.rotation);
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.tripleAngleOffSet));
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.tripleAngleOffSet));
            }
            else if (playerMod.twinShot)
            {
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - playerMod.twinAngleOffSet / 2));
                FireBullet(Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + playerMod.twinAngleOffSet / 2));
            }
            else
                FireBullet(transform.rotation);
            lastshot = Time.time;
            orbClock = 0f;
        }
    }

    void FireBullet(Quaternion rotation)
    {
        if (currentWeapon == PlayerMod.Weapon.Orb)
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
                {
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = rotation;
                    bullets[i].damage = (int)orbClock * playerMod.orbDamageAddedPerSecond + playerMod.orbBaselineDamage;
                    bullets[i].transform.localScale = new Vector3(playerMod.orbFinalSize, playerMod.orbFinalSize) * 
                        (orbClock / playerMod.orbMaxChargeDuration > playerMod.orbMinSize ? orbClock / playerMod.orbMaxChargeDuration : playerMod.orbMinSize);
                    bullets[i].tag = "Bullet";
                    return;
                }
            }
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
            {
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = rotation;
                bullets[i].tag = "Bullet";
                return;
            }
        }
    }

    void ChangeWeapon(PlayerMod.Weapon weapon)
    {
        currentWeapon = weapon;
        switch (weapon)
        {
            case PlayerMod.Weapon.Standard:
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = standardBullet;
                        bullets[i].speed = playerMod.standardProjectileSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.standardProjectileSize, playerMod.standardProjectileSize);
                        bullets[i].damage = playerMod.standardDamage;
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                shoodspeed = playerMod.standardAttackRate;
                break;
            case PlayerMod.Weapon.Orb:
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = orbBullet;
                        bullets[i].speed = playerMod.orbSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.orbMinSize, playerMod.orbMinSize);
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                break;
            case PlayerMod.Weapon.Boomerang:
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = boomerangBullet;
                        bullets[i].player = gameObject;
                        bullets[i].speed = playerMod.boomerangSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.boomerangSize, playerMod.boomerangSize);
                        bullets[i].tag = "WaitingToSpawn";
                        bullets[i].boomerangAcceleration = playerMod.boomerangAcceleration;
                        bullets[i].boomerangRange = playerMod.bommerangRange;
                        bullets[i].boomerangRotationSpeed = playerMod.boomerangRotationSpeed;
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                break;
            case PlayerMod.Weapon.Starburst:
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = starburstBullet;
                        bullets[i].burstAOEBase = playerMod.burstAOEBase;
                        bullets[i].burstAOEFactor = playerMod.burstAOEFactor;
                        bullets[i].burstSpeedFactor = playerMod.burstSpeedFactor;
                        bullets[i].speed = playerMod.burstSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.burstSize, playerMod.burstSize);
                        bullets[i].tag = "WaitingToSpawn";
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                break;
            case PlayerMod.Weapon.Arrow:
                shoodspeed = playerMod.arrowAttackSpeed;
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = arrowBullet;
                        bullets[i].speed = playerMod.arrowSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.arrowSize, playerMod.arrowSize);
                        bullets[i].tag = "WaitingToSpawn";
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                break;
            case PlayerMod.Weapon.Homing:
                for (int i = 0; i < bullets.Length; i++)
                {
                    if ((bullets[i].CompareTag("WaitingToSpawn") || bullets[i].CompareTag("Dead")) && bullets[i].speed > 0f)
                    {
                        bullets[i].currentWeapon = currentWeapon;
                        bullets[i].GetComponent<SpriteRenderer>().sprite = homingBullet;
                        bullets[i].homingAngle = playerMod.homingAngle;
                        bullets[i].homingRotationSpeed = playerMod.homingRotationSpeed;
                        bullets[i].homingDistance = playerMod.homingDistance;
                        bullets[i].speed = playerMod.homingSpeed;
                        bullets[i].transform.localScale = new Vector3(playerMod.homingSize, playerMod.homingSize);
                        bullets[i].tag = "WaitingToSpawn";
                    }
                    else if (bullets[i].CompareTag("Bullet") && bullets[i].speed > 0f)
                    {
                        bullets[i].tag = "Dead";
                        bullets[i].transform.position = bullets[i].deadPos;
                    }
                }
                break;
            default:
                break;

        }
        if (playerMod.bigShot)
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
                {
                    bullets[i].transform.localScale *= playerMod.projectileSize;
                }
            }
    }

    void MakeBigShot()
    {
        shoodspeed -= playerMod.attackSpeedReduction;
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
            {
                bullets[i].damage = playerMod.damage;
                bullets[i].transform.localScale *= playerMod.projectileSize;
                bullets[i].speed = playerMod.bigProjectileSpeed;
            }
        }
    }

    void MakeShortFuse()
    {
        shoodspeed = playerMod.attackSpeed;
        for (int i = 0; i < bullets.Length; i++)
        {
            if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
            {
                bullets[i].fuse = playerMod.fuse;
                bullets[i].speed = playerMod.shortProjectileSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && currentWeapon == PlayerMod.Weapon.Boomerang)
        {
            boomerangShot = false;
        }
    }
}
