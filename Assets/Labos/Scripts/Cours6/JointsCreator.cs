using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JointsCreator : MonoBehaviour
{ 
    Rigidbody rb;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }


    void Update()
    {
        Ray rayon = new Ray(transform.position, transform.up);
        Debug.DrawLine(rayon.origin, rayon.origin + rayon.direction * 10f, Color.yellow);
        
        if (Physics.Raycast(rayon, out RaycastHit hitInfo))
        {
            Debug.DrawLine(rayon.origin, hitInfo.point, Color.green);
            Rigidbody hitBody = hitInfo.collider.GetComponent<Rigidbody>();
            if (!hitBody)
                return;
            if(hitInfo.transform.gameObject.GetComponent<SpringJoint>())
                return;
            SpringJoint sJoint = hitInfo.transform.gameObject.AddComponent<SpringJoint>();
            sJoint.connectedBody = rb;
            
        }
    }
}
