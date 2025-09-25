using System;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private SphereCollider objCollider;
    private SphereCollider[] otherColliders;
    private float radius;
    private Vector3 startPoint;
    private Vector3 endPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objCollider = GetComponent<SphereCollider>();
        radius = objCollider.radius;
        
        startPoint = Vector3.zero;
        endPoint = Vector3.zero;
        
        //otherColliders = FindObjectsByType<SphereCollider>(FindObjectsSortMode.None);
        otherColliders = FindObjectsOfType<SphereCollider>();

        foreach (SphereCollider c in otherColliders)
        {
            Debug.Log(c.gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectCollisions();
    }

    void DetectCollisions()
    {
        foreach (SphereCollider otherCollider in otherColliders)
        {
            if (otherCollider.gameObject == objCollider.gameObject)
                break;

            radius = objCollider.radius;
            float otherRadius = otherCollider.radius;
            Transform otherTransform = otherCollider.transform;
            
            float distance = Vector3.Distance(transform.position, otherTransform.position);

            if (distance < (radius + otherRadius))
            {
                Vector3 direction = otherTransform.position - transform.position;
                Vector3 normal = (distance > 0f) ? direction / distance : direction;
                
                startPoint = otherTransform.position - normal * radius;
                endPoint = transform.position + normal * radius;
            }
            else
            {
                startPoint = Vector3.zero;
                endPoint = Vector3.zero;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPoint, endPoint);
    }
}
