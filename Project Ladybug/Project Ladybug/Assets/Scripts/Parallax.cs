using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed;
    private float counter;
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        counter = 0f;
    }

    
    void Update()
    {
        counter +=  speed * Time.deltaTime;
        float temp = (counter * (1 - parallaxEffect));
        float dist = (counter * parallaxEffect);
        transform.position = new Vector3(transform.position.x, startpos+dist, transform.position.z);

        
        /*if (transform.position.y > -1.9921f * 1.5f)
        {
            transform.localPosition += new Vector3(0f, 3.9031f, 0f);
            Debug.Log(this);
            Debug.Log("Jumped");
        }*/

    }
}
