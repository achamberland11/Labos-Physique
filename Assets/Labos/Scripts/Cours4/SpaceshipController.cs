using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : SpaceshipControllerBase
{
    [SerializeField] private float force = 25f;
    private void Start()
    {
        foreach (RocketEngine engine in _RocketEngines)
        {
            Debug.Log(engine.gameObject.name);
            engine.EnablePropulsion(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 shipToTargetVector = _TargetPosition - transform.position;
        
        _RocketEngines[0].Thrust(9.81f);
        

        if (shipToTargetVector.y > 0f)
        {
            _RocketEngines[0].Thrust(force);
        }
        else
        {
            _RocketEngines[3].Thrust(force);
        }
        
        if (shipToTargetVector.x < 0f)
        {
            _RocketEngines[1].Thrust(force);
        }
        else
        {
            _RocketEngines[2].Thrust(force);
        }
    }
    
}
