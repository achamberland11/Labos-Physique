using UnityEngine;

public class SpaceshipController : SpaceshipControllerBase
{
    [SerializeField] private const float BASEFORCE = 5f;
    [SerializeField] private const float DAMPING_FACTOR = 0.1f;
    private float forceX;
    private float forceY;
    float _gravity = 9.81f;

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

        // Apply force relative to distance
        forceY = BASEFORCE * Mathf.Abs(shipToTargetVector.y) *0.25f;
        forceX = BASEFORCE * Mathf.Abs(shipToTargetVector.x) *0.25f;

        Vector3 direction = shipToTargetVector.normalized;
    
        // Apply force with normalized distance
        //forceY = BASEFORCE * Mathf.Abs(direction.y);
        //forceX = BASEFORCE * Mathf.Abs(direction.x);
        
        ApplyThrust(shipToTargetVector);
    }


    void ApplyThrust(Vector3 shipToTargetVector)
    {
        if (shipToTargetVector.y > 0f)
        {
            _RocketEngines[0].Thrust(forceY + _gravity * (1 - DAMPING_FACTOR));
        }
        else
        {
            _RocketEngines[3].Thrust(forceY - _gravity * (1 - DAMPING_FACTOR));
        }

        if (shipToTargetVector.x < 0f)
        {
            _RocketEngines[1].Thrust(forceX * (1 - DAMPING_FACTOR));
        }
        else
        {
            _RocketEngines[2].Thrust(forceX * (1 - DAMPING_FACTOR));
        }
    }
    
}
