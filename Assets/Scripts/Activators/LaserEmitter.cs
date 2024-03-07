using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserEmitter : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public GameObject back;
    public GameObject front;

    public float distance;

    Vector3 dir;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (-front.transform.position + back.transform.position).normalized;
        
        int layerMask = 1 << 2;
        distance = 100.0f;

        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            distance = hit.distance;
           
            Debug.Log(hit.transform.position);
            Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);

        }

        target = transform.position + dir * distance;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1,  target);
    }

    void FixedUpdate()
    {
        
    }
}
