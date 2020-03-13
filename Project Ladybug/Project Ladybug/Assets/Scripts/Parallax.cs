using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Renderer renderer;
    public float speed = 0;
    private float counter;
    private float length, startpos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        renderer = GetComponent<Renderer>();
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

        //if (temp > startpos + length) startpos += length;
        //else if (temp < startpos - length) startpos -= length;

        
        /*if (transform.position.y > -1.9921f * 1.5f)
        {
            transform.localPosition += new Vector3(0f, 3.9031f, 0f);
            Debug.Log(this);
            Debug.Log("Jumped");
        }*/

    }
}
