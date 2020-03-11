using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMod : MonoBehaviour
{
    public enum Weapon { Standard, Orb, Boomerang, Starburst, Arrow, Homing }
    public Weapon currentWeapon;
    public bool healthy, twinShot, tripleShot, bigShot, shortFuse, wet, rest;
    public int maxHPadded;
    public int twinAngleOffSet;
    public int tripleAngleOffSet;
    void Start()
    {
        currentWeapon = Weapon.Standard;
        healthy = twinShot = tripleShot = bigShot = shortFuse = wet = rest = false;
    }
}
