using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypointsList;

    [SerializeField] float moveSpeed = 2f;

    //index of the current waypoint the enemy is moving towards
    int waypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       //set the enemy's position to the first waypoint
       transform.position = waypointsList[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }
    void EnemyMovement()
    {
        //if the enemy has not reached the last waypoint
        if (waypointIndex < waypointsList.Count)
        {
            //get the position of the next waypoint
            var targetPosition = waypointsList[waypointIndex].transform.position;

            targetPosition.z = 0f; //keep z position at 0 for 2D

            var movementThisFrame = moveSpeed * Time.deltaTime;
            //move the enemy towards the next waypoint
            transform.position = Vector2.MoveTowards(transform.position,
                targetPosition, movementThisFrame);
            //if the enemy has reached the current waypiont
            if (transform.position == targetPosition)
            {
                //increment the waypoint index to move to the next waypoint
                waypointIndex++;
            }
            
        }
        else //reach the last waypoint
        {
            Destroy(gameObject); // destroy the enemy object
        }
    }
}
