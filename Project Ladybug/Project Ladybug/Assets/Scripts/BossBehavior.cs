using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private float shootRange;
    [SerializeField] private int numOfShots;
    [SerializeField] private float randRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private SpriteRenderer otherThing;
    [SerializeField] private int MaxHp;
    [SerializeField] private Transform thing;
    private AudioSource oufer;
    private float attackCD;
    private bool inFight;
    private float counter;
    private int hp;
    private float startScale, startPos;

    private Collider collider;
    void Start()
    {
        collider = GetComponent<Collider>();
        oufer = GetComponent<AudioSource>();
        transform.localPosition = startPosition;
        hp = MaxHp;
        inFight = false;
        attackCD = 1 / attackSpeed;
        startScale = otherThing.transform.localScale.x;
        startPos = otherThing.transform.localPosition.x;
    }

    void Update()
    {
        if (!inFight) return;
        transform.position = Vector2.Lerp(transform.position, endPosition, lerpSpeed * Time.deltaTime);
        
        bool closeEnough = (((Vector2)transform.position - (Vector2)endPosition).magnitude < 1f);
        collider.enabled = closeEnough;

        if (!closeEnough) return;

        counter += Time.deltaTime;
        if (inFight && counter > attackCD)
        {
            float randOffSet = Random.Range(-randRange / 2f, randRange / 2);
            for (int i = 0; i < numOfShots; i++)
            {
                Shoot(Quaternion.Euler(0f, 0f, randOffSet - (shootRange / 2) + i * (shootRange / numOfShots)));
            }
            counter = 0f;
        }
    }

    public void Spawn()
    {
        print("Spawned Boss");
        transform.localPosition = startPosition;

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
        otherThing.transform.localScale = new Vector3(startScale * hp/ MaxHp,
            otherThing.transform.localScale.y, otherThing.transform.localScale.z);
    }

    private void Shoot(Quaternion bulletDirection)
    {
        Projectile[] bullets = FindObjectsOfType<Projectile>();
        oufer.Play();
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