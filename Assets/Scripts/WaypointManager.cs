using UnityEngine;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{

    [SerializeField] public List<Transform> wayPoints;
    public bool isMoving;
    public int waypointIndex;
    public float moveSpeed;
    public float rotationSpeed; 

    void Start()
    {
        StartMoving();
    }

    public void StartMoving()
    {
        waypointIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;
        

        if (waypointIndex < wayPoints.Count)
        {
            Vector3 target = wayPoints[waypointIndex].position;

            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed);

            Vector3 direction = target - transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            float distance = Vector3.Distance(transform.position, target);

            if (distance <= 0.05f)
            {
                waypointIndex++;
            }
        }
        else
        {
            isMoving = false;
        }

    }
}
