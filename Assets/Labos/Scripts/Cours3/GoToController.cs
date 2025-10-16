using UnityEngine;

public class GoToController : MonoBehaviour
{
    [SerializeField] Transform cible;
    [SerializeField] float force;
    [SerializeField] float treshHold;

    Rigidbody rb;
    Vector3 vPosCible;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vPosCible = cible.position - transform.position;
        
        if (Vector3.Distance(transform.position, cible.position) > treshHold)
        {
            rb.AddForce(vPosCible * force, ForceMode.Acceleration);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}