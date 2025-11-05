using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipController2 : SpaceshipController1 
{

    protected void Start()
    {
        base.Start();

        _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0f, 360f));
    }
    
    protected override void ApplyThrust(Vector3 shipToTargetVector)
    {
        Vector3 posEngine0 = _RocketEngines[0].gameObject.transform.position;
        Vector3 posEngine1 = _RocketEngines[1].gameObject.transform.position;
        Vector3 posEngine2 = _RocketEngines[2].gameObject.transform.position;
        Vector3 posEngine3 = _RocketEngines[3].gameObject.transform.position;

        if (Mathf.Abs(posEngine0.y) > Mathf.Abs(posEngine1.y))
        {
            // Debug.Log(_RocketEngines[0].gameObject.name + " and " + _RocketEngines[3].gameObject.name + " are on the y axis");
            // Debug.Log(_RocketEngines[1].gameObject.name + " and " + _RocketEngines[2].gameObject.name + " are on the x axis");
            
            // Up or Down
            if (shipToTargetVector.y > 0f)
            {
                if (posEngine0.y < posEngine3.y)
                {
                    _RocketEngines[0].Thrust(forceY + GRAVITY);
                    _RocketEngines[3].Thrust(0f);
                }
                else
                {
                    _RocketEngines[3].Thrust(forceY + GRAVITY);
                    _RocketEngines[0].Thrust(0f);
                }
            }
            else
            {
                if (posEngine0.y > posEngine3.y)
                {
                    _RocketEngines[0].Thrust(forceY);
                    _RocketEngines[3].Thrust(GRAVITY);
                }
                else
                {
                    _RocketEngines[3].Thrust(forceY);
                    _RocketEngines[0].Thrust(GRAVITY);
                }
            }
    
            // Left or Right
            if (shipToTargetVector.x > 0f)
            {
                // Debug.Log("Target is on the right");
                if (posEngine1.x < posEngine2.x)
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
            else
            {
                // Debug.Log("Target is on the left");
                if (posEngine1.x > posEngine2.x)
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
        }
        else
        {
            // Debug.Log(_RocketEngines[1].gameObject.name + " and " + _RocketEngines[2].gameObject.name + " are on the y axis");
            // Debug.Log(_RocketEngines[0].gameObject.name + " and " + _RocketEngines[3].gameObject.name + " are on the x axis");
            
            // Up or Down
            if (shipToTargetVector.y > 0f)
            {
                if (posEngine1.y < posEngine2.y)
                {
                    _RocketEngines[1].Thrust(forceY + GRAVITY);
                    _RocketEngines[2].Thrust(0f);
                }
                else
                {
                    _RocketEngines[2].Thrust(forceY + GRAVITY);
                    _RocketEngines[1].Thrust(0f);
                }
            }
            else
            {
                if (posEngine1.y > posEngine2.y)
                {
                    _RocketEngines[1].Thrust(forceY);
                    _RocketEngines[2].Thrust(GRAVITY);
                }
                else
                {
                    _RocketEngines[2].Thrust(forceY);
                    _RocketEngines[1].Thrust(GRAVITY);
                }
            }

            // Left or Right
            if (shipToTargetVector.x > 0f)
            {
                // Debug.Log("Target is on the right");
                if (posEngine0.x < posEngine3.x)
                {
                    _RocketEngines[0].Thrust(forceX);
                    _RocketEngines[3].Thrust(0f);
                }
                else
                {
                    _RocketEngines[3].Thrust(forceX);
                    _RocketEngines[0].Thrust(0f);
                }
            }
            else
            {
                // Debug.Log("Target is on the left");
                if (posEngine0.x > posEngine3.x)
                {
                    _RocketEngines[0].Thrust(forceX);
                    _RocketEngines[3].Thrust(0f);
                }
                else
                {
                    _RocketEngines[3].Thrust(forceX);
                    _RocketEngines[0].Thrust(0f);
                }
            }
        }
    }
}