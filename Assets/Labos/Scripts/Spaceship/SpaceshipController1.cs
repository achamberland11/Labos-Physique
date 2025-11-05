using UnityEngine;

public class SpaceshipController1 : SpaceshipControllerBase
{
    protected const float BASEFORCE = 10f;
    protected float forceX;
    protected float forceY;
    protected const float GRAVITY = 9.81f;
    protected bool bEngine0Thrusting;
    protected bool bEngine1Thrusting;
    protected bool bEngine2Thrusting;
    protected bool bEngine3Thrusting;

    protected void Start()
    {
        foreach (RocketEngine engine in _RocketEngines)
        {
            engine.EnablePropulsion(true);
        }
    }

    protected void Update()
    {
        base.Update();
        
        if (bEngine0Thrusting)
            _RocketEngines[0].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            _RocketEngines[0].gameObject.GetComponent<Renderer>().material.color = Color.red;


        if (bEngine1Thrusting)
            _RocketEngines[1].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            _RocketEngines[1].gameObject.GetComponent<Renderer>().material.color = Color.red;


        if (bEngine2Thrusting)
            _RocketEngines[2].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            _RocketEngines[2].gameObject.GetComponent<Renderer>().material.color = Color.red;


        if (bEngine3Thrusting)
            _RocketEngines[3].gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        else
            _RocketEngines[3].gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    
    protected void FixedUpdate()
    {
        Vector3 shipToTargetVector = _TargetPosition - transform.position;
        
        bEngine0Thrusting = false;
        bEngine1Thrusting = false;
        bEngine2Thrusting = false;
        bEngine3Thrusting = false;

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
            bEngine0Thrusting = true;
            bEngine3Thrusting = false;
        }
        else
        {
            _RocketEngines[3].Thrust(forceY);
            bEngine3Thrusting = true;
            bEngine0Thrusting = false;
        }

        if (shipToTargetVector.x < 0f)
        {
            _RocketEngines[1].Thrust(forceX);
            bEngine1Thrusting = true;
            bEngine2Thrusting = false;
        }
        else
        {
            _RocketEngines[2].Thrust(forceX);
            bEngine2Thrusting = true;
            bEngine1Thrusting = false;
        }
    }
    
}