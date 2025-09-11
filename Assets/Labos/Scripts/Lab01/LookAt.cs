using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform cible;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cible);     
    }
}
