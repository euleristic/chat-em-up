using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooty : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private AudioSource source;
    public float shoodspeed;
    private float lastshot;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        Projectile[] bullets = FindObjectsOfType<Projectile>();
        if(Input.GetButtonDown("Jump") && Time.time > lastshot + shoodspeed)
        {
            source.Play();
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].CompareTag("WaitingToSpawn") && bullets[i].speed > 0f)
                {
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = transform.rotation;
                    bullets[i].tag = "Bullet";
                    break;
                }
            }
            lastshot = Time.time;
        }
    }
}
