using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipController4 : SpaceshipController2
{
    private PayloadComponent Payload;

    protected void Start()
    {
        base.Start();
        _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        Payload = FindPayload();
    }

    protected void LateUpdate()
    {
        Debug.DrawLine(Payload.transform.position, FindPayloadGoal(), Color.green);
    }
    
    private void FixedUpdate()
    {
        Debug.DrawLine(transform.position, Payload.transform.position, Color.blue);
        Vector3 payloadToGoalVector = FindPayloadGoal() - Payload.transform.position;
        
        // Apply force relative to distance
        forceY = BASEFORCE + (Mathf.Abs(payloadToGoalVector.y) *0.5f);
        forceX = BASEFORCE + (Mathf.Abs(payloadToGoalVector.x) *0.5f);

        ApplyThrust(payloadToGoalVector);
    }
    
    PayloadComponent FindPayload()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 500f);
        foreach (Collider col in hits)
        {
            PayloadComponent payload = col.GetComponent<PayloadComponent>();
            if (payload != null)
            {
                Rigidbody payloadBody = payload.gameObject.GetComponent<Rigidbody>();
                if (!payloadBody)
                    return payload;

                payloadBody.constraints = RigidbodyConstraints.FreezePositionZ;
                
                if(payload.gameObject.GetComponent<SpringJoint>())
                    return payload;
                
                SpringJoint sJoint = payload.gameObject.AddComponent<SpringJoint>();
                sJoint.connectedBody = _Rigidbody;
                sJoint.anchor = Vector3.zero;
                sJoint.autoConfigureConnectedAnchor = false;
                sJoint.connectedAnchor = Vector3.zero;
                sJoint.spring = 10f;
                sJoint.minDistance = 2.0f;
                sJoint.maxDistance = 8.0f;
                return payload;
            }
        }

        return null;
    }
 
    
    
    Vector3 FindPayloadGoal()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 500f);
        foreach (var col in hits)
        {
            var goal = col.GetComponent<PayloadGoalComponent>();
            if (goal != null)
            {
                Debug.DrawLine(transform.position, goal.transform.position, Color.yellow);
                return goal.transform.position;
            }
        }

        return Vector3.zero;
    }
}