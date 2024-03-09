using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentCollisionTest : MonoBehaviour
{
    [SerializeField] Material noCollisionMaterial;
    [SerializeField] Material collisionMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<MeshRenderer>().material = collisionMaterial;  
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.GetComponent<MeshRenderer>().material = noCollisionMaterial;
    }
}
