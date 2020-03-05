using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> morepaths;
    [SerializeField] float movespeed = 3f;
    int anotherpathsIndex = 0; 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = morepaths[anotherpathsIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (anotherpathsIndex <= morepaths.Count - 1)
        {
            var targetPosition = morepaths[anotherpathsIndex].transform.position;
            var movementThisFrame = movespeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                anotherpathsIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
