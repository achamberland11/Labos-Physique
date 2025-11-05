using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipController3 : SpaceshipController1 
{

    protected void Start()
    {
        base.Start();
        _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        Vector3 shipToGoalVector = FindPath(FindGoal()) - transform.position;
        
        bEngine0Thrusting = false;
        bEngine1Thrusting = false;
        bEngine2Thrusting = false;
        bEngine3Thrusting = false;

        // Apply force relative to distance
        forceY = BASEFORCE + (Mathf.Abs(shipToGoalVector.y) *0.5f);
        forceX = BASEFORCE + (Mathf.Abs(shipToGoalVector.x) *0.5f);

        ApplyThrust(shipToGoalVector);
    }
    
    protected override void ApplyThrust(Vector3 shipToGoalVector)
    {
        base.ApplyThrust(shipToGoalVector);
    }

    Vector3 FindGoal()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 500f);
        foreach (var col in hits)
        {
            var goal = col.GetComponent<GoalComponent>();
            if (goal != null)
            {
                Debug.DrawLine(transform.position, goal.transform.position, Color.yellow);
                return goal.transform.position;
            }
        }

        return Vector3.zero;
    }

    Vector3 FindPath(Vector3 goalPosition)
    {
        if (Physics.Raycast(transform.position, goalPosition - transform.position, out RaycastHit hit))
        { 
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (hit.collider.gameObject.GetComponent<GoalComponent>())
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                return goalPosition;
            }
            else
            {
                float obstacleYExtents = hit.collider.bounds.extents.y;
                Vector3 obstaclePosition = hit.point;
                float spaceShipXExtents = transform.GetComponent<Collider>().bounds.extents.x;
                float extents = obstacleYExtents + spaceShipXExtents*2;
                
                Vector3 destinationA = obstaclePosition + (hit.collider.transform.right * extents);
                Vector3 destinationB = obstaclePosition - (hit.collider.transform.right * extents);
               
                bool clearA = !Physics.Raycast(
                    transform.position,
                    (destinationA - transform.position).normalized,
                    Vector3.Distance(transform.position, destinationA)); 
               
                bool clearB = !Physics.Raycast(
                    transform.position,
                    (destinationB - transform.position).normalized,
                    Vector3.Distance(transform.position, destinationB));

                if (clearA && clearB)
                {
                    if (Vector3.Distance(transform.position, destinationA) <
                        Vector3.Distance(transform.position, destinationB))
                    {
                        Debug.DrawLine(transform.position, destinationA, Color.blue);
                        return destinationA;
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, destinationB, Color.blue);
                        return destinationB;
                    }
                }

                if (clearA)
                {
                    Debug.DrawLine(transform.position, destinationA, Color.blue);
                    return destinationA;
                }
                else if (clearB)
                {
                    Debug.DrawLine(transform.position, destinationB, Color.blue);
                    return destinationB;
                }
                
            }
        }
        
        return Vector3.zero;
    }
}