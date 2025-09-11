using UnityEngine;

public class Orbite : MonoBehaviour
{
    [SerializeField] Transform corpsCentral;
    [SerializeField] float vitesseRotation = 30f;
    float distance;
    float angle;
    float omega;
    Vector2 offsetXZ;

    float newX;
    float newZ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        distance = CalculerMagnitude(transform.position, corpsCentral.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offsetXZ = new Vector2(
                transform.position.x - corpsCentral.position.x,
                transform.position.z - corpsCentral.position.z);
        angle = Mathf.Atan2(offsetXZ.y, offsetXZ.x);

        omega = vitesseRotation * Mathf.Deg2Rad;
        angle += omega * Time.deltaTime;

        newX = corpsCentral.position.x + distance * Mathf.Cos(angle);
        newZ = corpsCentral.position.z + distance * Mathf.Sin(angle);

        transform.position = new Vector3(newX, transform.position.y, newZ);
    }

    float CalculerMagnitude(Vector3 pointA, Vector3 pointB)
    {
        float x = pointB.x - pointA.x;
        float y = pointB.z - pointA.z;

        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }
}
