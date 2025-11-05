using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

public class SpaceshipController1 : SpaceshipControllerBase
{
    protected const float BASEFORCE = 10f;
    protected float forceX;
    protected float forceY;
    protected const float GRAVITY = 9.81f;
    

    protected void Start()
    {
        foreach (RocketEngine engine in _RocketEngines)
        {
            engine.EnablePropulsion(true);
        }
    }
    
    protected void FixedUpdate()
    {
        Vector3 shipToTargetVector = _TargetPosition - transform.position;
        
        // Apply force relative to distance
        forceY = BASEFORCE + (Mathf.Abs(shipToTargetVector.y) *0.5f);
        forceX = BASEFORCE + (Mathf.Abs(shipToTargetVector.x) *0.5f);
       
        ApplyThrust(shipToTargetVector);
    }


    protected virtual void ApplyThrust(Vector3 shipToTargetVector)
    {
        if (shipToTargetVector.y > 0f)
        {
            _RocketEngines[0].Thrust(forceY + GRAVITY);
            _RocketEngines[3].Thrust(0f);
        }
        else
        {
            _RocketEngines[3].Thrust(forceY);
            _RocketEngines[0].Thrust(GRAVITY);
        }

        if (shipToTargetVector.x < 0f)
        {
            _RocketEngines[1].Thrust(forceX);
            _RocketEngines[2].Thrust(0f);
        }
        else
        {
            _RocketEngines[2].Thrust(forceX);
            _RocketEngines[1].Thrust(0f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.GetComponentInParent<GoalComponent>())
            return;
        
        Vector3 normal = other.contacts[0].normal;

        // Check the direction of the normal to determine which side was hit
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        {
            // Left or right side
            if (normal.x < 0)
            {
                // Right side hit
                _RocketEngines[1].Thrust(forceX + forceX/2);
                _RocketEngines[2].Thrust(0f);
                Debug.Log("Right side hit");
            }
            else
            {
                // Left side hit
                _RocketEngines[2].Thrust(forceX + forceX/2);
                _RocketEngines[1].Thrust(0f);
                Debug.Log("Left side hit");
            }
        }
        else
        {
            // Up or down side
            if (normal.y < 0)
            {
                _RocketEngines[3].Thrust(forceY + forceY/2);
                _RocketEngines[0].Thrust(0f);
                Debug.Log("Up side hit");
            }
            else
            {
                // Down side hit
                _RocketEngines[0].Thrust(forceY + forceY/2 + GRAVITY);
                _RocketEngines[3].Thrust(0f);
                Debug.Log("Down side hit");
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!_Rigidbody)
            return;
        string msg = "Vélocité : " + _Rigidbody.linearVelocity;

        Vector3 labelPosition = transform.position + Vector3.up;

        Handles.color = Color.blue;
        
        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 10;          // <-- your desired size
        labelStyle.normal.textColor = Color.blue;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        Handles.Label(labelPosition, msg, labelStyle);
    }
}