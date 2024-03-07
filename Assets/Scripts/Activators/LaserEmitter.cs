using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserEmitter : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public GameObject back;
    public GameObject front;

    public float distance;

    public Vector3 dir;
    public Vector3 target;

    public Transform reflectionEmmiter = null;
    public LaserEmitter parentLaserEmitter = null;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Transform reflectionEmmiter = null;
        LaserEmitter parentLaserEmitter = null;
    }

    // Update is called once per frame
    void Update()
    {

        Transform current = reflectionEmmiter;
        Transform next = null;
       
        if((current != null) && (current.GetComponent<LaserEmitter>() != null) && (current.GetComponent<LaserEmitter>().reflectionEmmiter != null))
        {
            next = current.GetComponent<LaserEmitter>().reflectionEmmiter;
        }
        
        while (current != null) 
        {
            Destroy(current.GetComponent<LaserEmitter>());
            Destroy(current.GetComponent<LineRenderer>());
            
            current = next;
            if ((current != null) && (current.GetComponent<LaserEmitter>() != null) && (current.GetComponent<LaserEmitter>().reflectionEmmiter != null))
            {
                next = current.GetComponent<LaserEmitter>().reflectionEmmiter;
            }
        }


        dir = (back.transform.position - front.transform.position).normalized;

        int layerMask = 1 << 2;
        distance = 100.0f;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            distance = hit.distance;

            if (hit.transform.tag == "MirrorEffect")
            {
                target = transform.position + dir * distance;
                Debug.DrawRay(transform.position, dir * hit.distance, Color.yellow);

                Vector2 hit_position = new Vector2(target.x, target.z);
                Vector2 A = new Vector2(hit.transform.GetChild(0).position.x, hit.transform.GetChild(0).position.z);
                Vector2 B = new Vector2(hit.transform.GetChild(1).position.x, hit.transform.GetChild(1).position.z);

                Vector2 surface_direction = (B - A).normalized;

                float angle = Vector2.SignedAngle(surface_direction, new Vector2(dir.x, dir.z));
                float double_angle = angle * 2.0f;

                var rotation = Quaternion.AngleAxis(double_angle, Vector3.up);
                var new_dir_vector = rotation * dir;

                Debug.DrawRay(target, new_dir_vector * 100.0f, Color.yellow);

                reflectionEmmiter = hit.transform.gameObject.transform;
                var child = hit.transform.gameObject.transform;

                var childLaserEmitter = child.AddComponent<LaserEmitter>();
                var childLineRenderer = child.AddComponent<LineRenderer>();

                childLaserEmitter.parentLaserEmitter = this;
                childLaserEmitter.back = child.transform.GetChild(2).gameObject;
                childLaserEmitter.front = child.transform.GetChild(3).gameObject;
                childLaserEmitter.distance = 100.0f;
                childLaserEmitter.dir = new_dir_vector;
                childLaserEmitter.target = child.transform.position + new_dir_vector * distance;

                childLineRenderer.widthCurve = lineRenderer.widthCurve;
                childLineRenderer.material = lineRenderer.material;
                childLineRenderer.SetPosition(0, child.position);
                childLineRenderer.SetPosition(1, childLaserEmitter.target);
            }
        }

        target = transform.position + dir * distance;


        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target);
    }

    void FixedUpdate()
    {
        
    }
}
