using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float speed;
    private Vector2 input;
    private Rigidbody rb;
    private bool invincible;
    
    [SerializeField] private float thrusterLow;
    [SerializeField] private float thrusterHigh;
    [SerializeField] private float thrusterLerp;
    [SerializeField] private AudioSource thruster;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float hitFadeIn;
    [SerializeField] private float hitFadeOut;
    [SerializeField] private float snappiness;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float airRes;
    [SerializeField] private Sprite heartSprite;
    [SerializeField] SpriteRenderer[] hearts = new SpriteRenderer[4];
    public float gravity;
    private bool healthy;
    private bool wet;
    private bool rest;
    [SerializeField] private PlayerMod playerMod;

    [SerializeField] private int maxHP;
    [SerializeField] private int hp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        invincible = false;
        hearts[3].sprite = null;
        healthy = wet = rest = false;
    }

    
    void Update()
    {
        if (!healthy && playerMod.healthy)
        {
            maxHP += playerMod.maxHPadded;
            hp += playerMod.maxHPadded;
            healthy = true;
        }

        if (!wet && playerMod.wet)
        {
            speed = playerMod.movementSpeed;
            snappiness = playerMod.turningSpeed;
            airRes = playerMod.airResistance;
            wet = true;
        }

        if (!rest && playerMod.rest)
        {
            if (playerMod.allHP)
                hp = maxHP;
            else
                hp += playerMod.HPrestored;
            if (hp > maxHP)
                hp = maxHP;
            rest = true;
        }

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        rb.velocity += new Vector3(input.x * snappiness, input.y * snappiness, 0f);
        if (rb.velocity.magnitude > speed)
            rb.velocity = rb.velocity.normalized * speed;
        if (input.x == 0f && input.y == 0f)
            rb.velocity /= airRes;

        float rotation = 0f;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f)
            rotation = rotationAmount * -Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f, rotation), rotationSpeed * Time.deltaTime);

        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            thruster.volume += (thrusterHigh - thruster.volume) * thrusterLerp;
        else
            thruster.volume += (thrusterLow - thruster.volume) * thrusterLerp;

        //hitflash
        if (invincible)
        {
            sr.color = Color.Lerp(sr.color, Color.white, hitFadeIn);
            if ((Color.white - sr.color).a < 0.1)
            {
                sr.color = Color.white;
                invincible = false;
            }
        }
        else if (sr.color != Color.clear)
        {
            sr.color = Color.Lerp(sr.color, Color.clear, hitFadeOut);
            if (!invincible && (sr.color - Color.clear).a < 0.1)
            {
                sr.color = Color.clear;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("EnemyBullet") || other.CompareTag("AliveEnemy")) && !invincible)
        {
            hp--;
            invincible = true;
        }
        if (hp <= 0)
            SceneScript.LoseState();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
                hearts[i].sprite = heartSprite;
            else
                hearts[i].sprite = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("AliveEnemy") && invincible) ;
            

    }
}