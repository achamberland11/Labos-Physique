using System;
using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] ForceMode mode;
    
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
        rb.AddForce(transform.forward, mode);
    }
}