using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMod : MonoBehaviour
{
    public enum Weapon { Standard, Orb, Boomerang, Starburst, Arrow, Homing }
    public Weapon currentWeapon;
    public bool healthy, twinShot, tripleShot, bigShot, shortFuse, wet, rest;
    [Header("Healthy")]
    public int maxHPadded;
    [Header("Twin Shot")]
    public float twinAngleOffSet;
    [Header("Triple Shot")]
    public float tripleAngleOffSet;
    [Header("Big Shot")]
    public float projectileSize;
    public float bigProjectileSpeed;
    public int damage;
    public float attackSpeedReduction;
    [Header("Short Fuse")]
    public float attackSpeed;
    public float fuse;
    public float shortProjectileSpeed;
    [Header("Wet")]
    public float movementSpeed;
    public float turningSpeed;
    public float airResistance;
    [Header("Rest")]
    public int HPrestored;
    public bool allHP;

    [Header("Standard")]
    public float standardProjectileSpeed;
    public float standardAttackRate;
    public float standardProjectileSize;
    public int standardDamage;
    [Header("Orb")]
    public float orbChargeSpeed;
    public float orbMaxChargeDuration;
    public int orbDamageAddedPerSecond;
    public int orbBaselineDamage;
    public float orbFinalSize;
    [Header("Boomerang")]
    public float bommerangRange;
    public float boomerangRotationSpeed;
    public float boomerangAcceleration;
    [Header("Starburst")]
    public float burstAOEBase;
    public float burstAOEFactor;
    public float burstSpeedFactor;
    [Header("Arrow")]
    public float arrowAttackSpeed;
    public int arrowBulletsPerBurst;
    public float arrowDelta;
    [Header("Homing")]
    public float homingAngle;
    public float homingRotationSpeed;
    public float homingDistance;
    void Start()
    {
        currentWeapon = Weapon.Standard;
        healthy = twinShot = tripleShot = bigShot = shortFuse = wet = rest = false;
    }
}
