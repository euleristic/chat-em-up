using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
       
    }
    
   
    void Update()
    {
        transform.position += new Vector3(0f, speed  * Time.deltaTime, 0f);
    }
}
