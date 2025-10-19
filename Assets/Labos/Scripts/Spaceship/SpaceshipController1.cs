using UnityEngine;

public class SpaceshipController1 : SpaceshipControllerBase
{
    private const float BASEFORCE = 8f;
    private const float DAMPING_FACTOR = 0.1f;
    private float forceX;
    private float forceY;
    float _gravity = 9.81f;
    bool bEngine0Thrusting;
    bool bEngine1Thrusting;
    bool bEngine2Thrusting;
    bool bEngine3Thrusting;

    private void Start()
    {
        foreach (RocketEngine engine in _RocketEngines)
        {
            Debug.Log(engine.gameObject.name);
            engine.EnablePropulsion(true);
        }
    }

    private void Update()
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
    private void FixedUpdate()
    {
        Vector3 shipToTargetVector = _TargetPosition - transform.position;

        // Apply force relative to distance
        forceY = BASEFORCE + (Mathf.Abs(shipToTargetVector.y) *0.25f);
        forceX = BASEFORCE + (Mathf.Abs(shipToTargetVector.x) *0.25f);
       
        ApplyThrust(shipToTargetVector);
    }


    void ApplyThrust(Vector3 shipToTargetVector)
    {
        if (shipToTargetVector.y > 0f)
        {
            _RocketEngines[0].Thrust(forceY + _gravity * (1 - DAMPING_FACTOR));
            bEngine0Thrusting = true;
            bEngine3Thrusting = false;
        }
        else
        {
            _RocketEngines[3].Thrust(forceY - _gravity * (1 - DAMPING_FACTOR));
            bEngine3Thrusting = true;
            bEngine0Thrusting = false;
        }

        if (shipToTargetVector.x < 0f)
        {
            _RocketEngines[1].Thrust(forceX * (1 - DAMPING_FACTOR));
            bEngine1Thrusting = true;
            bEngine2Thrusting = false;
        }
        else
        {
            _RocketEngines[2].Thrust(forceX * (1 - DAMPING_FACTOR));
            bEngine2Thrusting = true;
            bEngine1Thrusting = false;
        }
    }
    
}