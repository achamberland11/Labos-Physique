using Unity.VisualScripting;
using UnityEngine;

public class ForceApplierImproved : ForceApplier
{

    void Update()
    {
        Ray rayon = new Ray(transform.position, transform.right);
        Debug.DrawLine(rayon.origin, rayon.origin + rayon.direction * 100.0f, Color.green);
        
        if (Physics.Raycast(rayon, out RaycastHit hitInfo))
        {
            Debug.DrawLine(rayon.origin, hitInfo.point, Color.green);
            Rigidbody hitBody = hitInfo.collider.GetComponent<Rigidbody>();
            if (!hitBody)
                return;
            hitBody.AddForceAtPosition(rayon.direction, hitInfo.point, ForceMode.Force); 
        }
    }
}
