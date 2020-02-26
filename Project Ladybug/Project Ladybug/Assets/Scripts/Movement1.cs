using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float speed;
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] private float snappiness;
    public float gravity;
    [SerializeField] private float rotationAmount;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private int hp;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        rb.AddForce(input.x * snappiness, input.y * snappiness, 0f);
        if (rb.velocity.magnitude > speed)
            rb.velocity = rb.velocity.normalized * speed;

        float rotation = 0f;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.01f)
            rotation = rotationAmount * -Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f,0f, rotation), rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("AliveEnemy"))
            hp--;
        if (hp <= 0)
            SceneScript.LoseState();
    }
}
