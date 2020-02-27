using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 deadPos;


    void Start()
    {
        deadPos = transform.position;
    }
    void FixedUpdate()
    {
        if(!CompareTag("WaitingToSpawn"))
            transform.position += (transform.up * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
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
