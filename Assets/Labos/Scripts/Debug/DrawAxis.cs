using UnityEngine;

public class DrawAxis : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin, origin + transform.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + transform.right);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + transform.up);
    }
}
