using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float timeBtwFiring = 0.1f;

    Coroutine firingCorountine;

    float xmin;
    float xmax;
    float ymin;
    float ymax;

    void Start()
    {
        LimitBoundaries();
    }

    private void LimitBoundaries()
    {
        Camera gameCamera = Camera.main;
        xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }
    //Viewportto... sonverts the position fo something as it relates to camera veiw

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           firingCorountine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCorountine);
        }
    }

    IEnumerator FireContinuously()
    {
       while (true)
        { 
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(timeBtwFiring);
        }
     
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        //Debug.Log(deltaX);
        var newXposition = Mathf.Clamp(transform.position.x + deltaX, xmin, xmax);
        
        var deltaY = Input.GetAxis("Vertical");
        var newYposition = Mathf.Clamp(transform.position.y + deltaY, ymin, ymax);

        transform.position = new Vector2(newXposition, newYposition);
    }
}
