using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] GameObject pathObj;
    [SerializeField] float movementSpeed = 5.0f;
    [SerializeField] float waitForNextMovementTime = 1.5f;

    List<Transform> waypoints;
    int waypointIndex;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        canMove = true;
        waypointIndex = 0;
        waypoints = new List<Transform>();

        foreach (Transform childObj in pathObj.GetComponentInChildren<Transform>())
        {
            waypoints.Add(childObj.transform);
        }

        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        foreach (Transform waypoint in waypoints)
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = movementSpeed * Time.deltaTime;
                if(canMove)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);

                    if (transform.position == targetPosition)
                    {
                        StartCoroutine(WaitForMoveToNextTargetPosition());
                    }
                }

            }
            else
            {
                waypointIndex = 0;
            }
        }
    }

    private IEnumerator WaitForMoveToNextTargetPosition()
    {
        canMove = false;
        yield return new WaitForSeconds(waitForNextMovementTime);
        waypointIndex++;
        canMove = true;
    }
}
