using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HingeCreator : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float vRotation = 90f;
    private float angle = 0f;
    List<GameObject>  hinges = new List<GameObject>();
    
    [Header("Hinge Param")]
    [SerializeField] Vector3 axis = new Vector3(0,0,1);
    [SerializeField] bool bAutoConfig = true;
    [SerializeField] bool bUseMotor = true;
    [SerializeField] float targetVelocity = 100.0f;
    [SerializeField] float force = 10000.0f;
    
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        angle += vRotation * Time.deltaTime;
        angle %= 360f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 dir = rotation * Vector3.up;
        
        Ray rayon = new Ray(transform.position, dir);
        Debug.DrawLine(rayon.origin, rayon.origin + rayon.direction * 10f, Color.yellow);
        
        if (Physics.Raycast(rayon, out RaycastHit hitInfo))
        {
            Debug.DrawLine(rayon.origin, hitInfo.point, Color.green);
            Rigidbody hitBody = hitInfo.collider.GetComponent<Rigidbody>();
            if (!hitBody)
                return;
            if (hinges.Contains(hitBody.gameObject))
                return;
            hinges.Add(hitBody.gameObject);
            
            HingeJoint hJoint = transform.gameObject.AddComponent<HingeJoint>();
            hJoint.connectedBody = hitBody;
            hJoint.anchor = Vector3.zero;
            hJoint.axis = axis;
            hJoint.autoConfigureConnectedAnchor = bAutoConfig;
            hJoint.useMotor = bUseMotor;
            var motor = hJoint.motor;
            motor.targetVelocity = targetVelocity;
            motor.force = force;
            
            hJoint.motor = motor;
        }
    }
}
