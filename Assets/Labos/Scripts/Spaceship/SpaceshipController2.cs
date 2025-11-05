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
    
    /// <summary>
    /// Normalement, la fonction devrait trouver quels engines sont sur l'axe y et lesquels sont sur l'axe x.
    /// Ensuite le reste de la fonction devrait trouver le moteur le plus éloigné de la cible sur chaque axe et appliquer
    /// une force aux engines correspondantes, la force appliqué est proportionnel à la distance et la gravité est ajouté
    /// si nécessaire. Tout ce processus se fait correctement selon les Debug.Log mais le spaceship ne se dirige pas vers
    /// la cible pour une raison qui m'échappe.
    /// </summary>
    /// <param name="shipToTargetVector"></param>
    protected override void ApplyThrust(Vector3 shipToTargetVector)
    {
        Vector3 posEngine0 = _RocketEngines[0].gameObject.transform.position;
        Vector3 posEngine1 = _RocketEngines[1].gameObject.transform.position;
        Vector3 posEngine2 = _RocketEngines[2].gameObject.transform.position;
        Vector3 posEngine3 = _RocketEngines[3].gameObject.transform.position;

        if (Mathf.Abs(posEngine0.y) < Mathf.Abs(posEngine1.y))
        {
            Debug.Log(_RocketEngines[0].gameObject.name + " and " + _RocketEngines[3].gameObject.name + " are on the y axis");
            Debug.Log(_RocketEngines[1].gameObject.name + " and " + _RocketEngines[2].gameObject.name + " are on the x axis");
            
            // Up or Down
            if (shipToTargetVector.y > 0f)
            {
                if (posEngine0.y < posEngine3.y)
                {
                    _RocketEngines[0].Thrust(forceY + GRAVITY);
                    Debug.Log(_RocketEngines[0].gameObject.name + " Thrusting up with : " + (forceY + GRAVITY) + " force");
                }
                else
                {
                    _RocketEngines[3].Thrust(forceY + GRAVITY);
                    Debug.Log(_RocketEngines[3].gameObject.name + " Thrusting up with : " + (forceY + GRAVITY) + " force");
                }
            }
            else
            {
                if (posEngine0.y > posEngine3.y)
                {
                    _RocketEngines[0].Thrust(forceY);
                    Debug.Log(_RocketEngines[0].gameObject.name + " Thrusting down with : " + forceY + " force");
                }
                else
                {
                    _RocketEngines[3].Thrust(forceY);
                    Debug.Log(_RocketEngines[3].gameObject.name + " Thrusting down with : " + forceY + " force");
                }
            }
    
            // Left or Right
            if (shipToTargetVector.x > 0f)
            {
                if (posEngine1.x < posEngine2.x)
                {
                    _RocketEngines[1].Thrust(forceX);
                    Debug.Log(_RocketEngines[1].gameObject.name + " Thrusting right with : " + forceX + " force");
                }
                else
                {
                    _RocketEngines[2].Thrust(forceX);
                    Debug.Log(_RocketEngines[2].gameObject.name + " Thrusting right with : " + forceX + " force");
                }
            }
            else
            {
                if (posEngine1.x > posEngine2.x)
                {
                    _RocketEngines[1].Thrust(forceX);
                    Debug.Log(_RocketEngines[1].gameObject.name + " Thrusting left with : " + forceX + " force");
                }
                else
                {
                    _RocketEngines[2].Thrust(forceX);
                    Debug.Log(_RocketEngines[2].gameObject.name + " Thrusting left with : " + forceX + " force");
                }
            }
        }
        else
        {
            Debug.Log(_RocketEngines[1].gameObject.name + " and " + _RocketEngines[2].gameObject.name + " are on the y axis");
            Debug.Log(_RocketEngines[0].gameObject.name + " and " + _RocketEngines[3].gameObject.name + " are on the x axis");
            
            // Up or Down
            if (shipToTargetVector.y > 0f)
            {
                if (posEngine1.y < posEngine2.y)
                {
                    _RocketEngines[1].Thrust(forceY + GRAVITY);
                    Debug.Log(_RocketEngines[1].gameObject.name + " Thrusting up with : " + (forceY + GRAVITY) + " force");
                }
                else
                {
                    _RocketEngines[2].Thrust(forceY + GRAVITY);
                    Debug.Log(_RocketEngines[2].gameObject.name + " Thrusting up with : " + (forceY + GRAVITY) + " force");
                }
            }
            else
            {
                if (posEngine1.y > posEngine2.y)
                {
                    _RocketEngines[1].Thrust(forceY);
                    Debug.Log(_RocketEngines[1].gameObject.name + " Thrusting down with : " + forceY + " force");
                }
                else
                {
                    _RocketEngines[2].Thrust(forceY);
                    Debug.Log(_RocketEngines[2].gameObject.name + " Thrusting down with : " + forceY + " force");
                }
            }

            // Left or Right
            if (shipToTargetVector.x > 0f)
            {
                if (posEngine0.x > posEngine3.x)
                {
                    _RocketEngines[0].Thrust(forceX);
                    Debug.Log(_RocketEngines[0].gameObject.name + " Thrusting right with : " + forceY + " force");
                }
                else
                {
                    _RocketEngines[3].Thrust(forceX);
                    Debug.Log(_RocketEngines[3].gameObject.name + " Thrusting right with : " + forceY + " force");
                }
            }
            else
            {
                if (posEngine0.x < posEngine3.x)
                {
                    _RocketEngines[0].Thrust(forceX);
                    Debug.Log(_RocketEngines[0].gameObject.name + " Thrusting left with : " + forceY + " force");
                }
                else
                {
                    _RocketEngines[3].Thrust(forceX);
                    Debug.Log(_RocketEngines[2].gameObject.name + " Thrusting left with : " + forceY + " force");
                }
            }
        }
    }
}