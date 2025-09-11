using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform cible;
    Vector3 axeRotation;
    Vector3 offset;
    Vector3 direction;
    Quaternion offsetRotation;

    void Start()
    {
        offset = transform.position - cible.position;

        direction = Vector3.up;
        if (Mathf.Abs(Vector3.Dot(offset.normalized, direction)) > 0.99f)
            direction = Vector3.right;


        axeRotation = Vector3.Cross(offset, direction).normalized;
        Quaternion look0 = Quaternion.LookRotation((cible.position - transform.position).normalized, axeRotation);
        offsetRotation = Quaternion.Inverse(look0) * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion look = Quaternion.LookRotation((cible.position - transform.position).normalized, axeRotation);

        // Add the rotation offset to the lookat rotation.
        transform.rotation = look * offsetRotation;
    }
}
