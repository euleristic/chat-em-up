using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> Waypoint;
    [SerializeField] private float movespeed;
    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Waypoint = waveConfig.GetWayPoints();
        transform.position = Waypoint[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= Waypoint.Count - 1)
        {
            var targetPosition = Waypoint[waypointIndex].transform.position;
            var movementThisFrame = movespeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
