using UnityEngine;

public class ForceApplier : MonoBehaviour
{
     Rigidbody rb;
    [SerializeField] protected ForceMode mode;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyForce(transform.forward);
    }

    protected void ApplyForce(Vector3 v) 
    {
        rb.AddForce(v, mode);
    }
}
