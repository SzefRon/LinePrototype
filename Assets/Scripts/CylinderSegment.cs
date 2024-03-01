using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSegment : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    private Vector3 initial_position;
    private float initial_distance;

    void Start()
    {
        initial_position = transform.position;
        initial_distance = Vector3.Distance(object1.position, object2.position);
    }

    void FixedUpdate()
    {
        Vector3 midpoint = (object1.position + object2.position) / 2f;
        transform.position = midpoint;

        Vector3 direction = (object2.position - object1.position).normalized;
        Quaternion target_rotation = Quaternion.LookRotation(direction);
        transform.rotation = target_rotation;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(object1.position, object2.position));
    }
}
