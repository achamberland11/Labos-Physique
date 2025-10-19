using UnityEngine;

public class SpaceshipController2 : SpaceshipControllerBase
{
    private const float BASEFORCE = 8f;
    private float force;
    float _gravity = 9.81f;
    bool bEngine0Thrusting;
    bool bEngine1Thrusting;
    bool bEngine2Thrusting;
    bool bEngine3Thrusting;

    private void Start()
    {
        foreach (RocketEngine engine in _RocketEngines)
        {
            engine.EnablePropulsion(true);
        }
        
        _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0f, 360f));

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

        float lowestY = 0f;

        foreach (RocketEngine engine in _RocketEngines)
        {
            if (engine.gameObject.transform.position.y < lowestY)
            {
                lowestY = engine.gameObject.transform.position.y;
            }
        }

        ApplyThrust(shipToTargetVector, lowestY);
    }


    void ApplyThrust(Vector3 shipToTargetVector, float lowestY)
    {
        Vector3 posEngine0 = _RocketEngines[0].gameObject.transform.position;
        Vector3 posEngine1 = _RocketEngines[1].gameObject.transform.position;
        Vector3 posEngine2 = _RocketEngines[2].gameObject.transform.position;
        Vector3 posEngine3 = _RocketEngines[3].gameObject.transform.position;
        
        force = BASEFORCE + (shipToTargetVector.magnitude * 0.25f);

        if (Vector3.Distance(posEngine0, shipToTargetVector) > Vector3.Distance(posEngine3, shipToTargetVector))
        {
            if (posEngine0.y <= lowestY && transform.position.y < _TargetPosition.y)
                _RocketEngines[0].Thrust(force + _gravity);
            else
                _RocketEngines[0].Thrust(force);
            
            bEngine0Thrusting = true;
            bEngine3Thrusting = false;
        }
        else
        {
            if (posEngine3.y <= lowestY && transform.position.y < _TargetPosition.y)
                _RocketEngines[3].Thrust(force + _gravity);
            else
                _RocketEngines[3].Thrust(force);
            
            bEngine3Thrusting = true;
            bEngine0Thrusting = false;
        }

        if (Vector3.Distance(posEngine1, shipToTargetVector) > Vector3.Distance(posEngine2, shipToTargetVector))
        {
            if (posEngine3.y <= lowestY && transform.position.y < _TargetPosition.y)
                _RocketEngines[1].Thrust(force + _gravity);
            else
                _RocketEngines[1].Thrust(force);
            
            bEngine1Thrusting = true;
            bEngine2Thrusting = false;
        }
        else
        {
            if (posEngine2.y <= lowestY && transform.position.y < _TargetPosition.y)
                _RocketEngines[2].Thrust(force + _gravity);
            else
                _RocketEngines[2].Thrust(force);
            
            bEngine2Thrusting = true;
            bEngine1Thrusting = false;
        }
    }

}
