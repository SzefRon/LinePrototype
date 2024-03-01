using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject ropeSegmentPrefab;
    [SerializeField] private GameObject ropeSegmentPrefab2;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private uint segments;

    private List<GameObject> ballJoints = new();
    private List<GameObject> cylinderSegments = new();    

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody startRb = start.GetComponent<Rigidbody>();
        Rigidbody endRb = end.GetComponent<Rigidbody>();

        Vector3 p1 = start.transform.position;
        Vector3 p2 = end.transform.position;

        Vector3 direction = p2 - p1;
        uint div = segments + 1;
        Vector3 offset = direction / div;

        GameObject lastObj = start;
        for (uint i = 0; i <= div; i++) {
            GameObject segmentObject = Instantiate(ropeSegmentPrefab, p1 + offset * i, Quaternion.identity);
            segmentObject.transform.SetParent(transform);

            ballJoints.Add(segmentObject);

            ConfigurableJoint joint = segmentObject.GetComponent<ConfigurableJoint>();
            joint.connectedBody = lastObj.GetComponent<Rigidbody>();
            lastObj = segmentObject;
        }
        ConfigurableJoint endJoint = end.AddComponent<ConfigurableJoint>();
        endJoint.connectedBody = lastObj.GetComponent<Rigidbody>();
        ConfigurableJoint lastJoint = lastObj.GetComponent<ConfigurableJoint>();

        endJoint.linearLimit = lastJoint.linearLimit;
        endJoint.xMotion = lastJoint.xMotion;
        endJoint.yMotion = lastJoint.yMotion;
        endJoint.zMotion = lastJoint.zMotion;
        endJoint.linearLimitSpring = lastJoint.linearLimitSpring;

        for(int i = 0; i < ballJoints.Count - 1; i++)
        {
            GameObject first = ballJoints[i];
            GameObject second = ballJoints[i + 1];
            Vector3 position = Vector3.Lerp(first.transform.position, second.transform.position, 0.5f);

            GameObject segment = Instantiate(ropeSegmentPrefab2, position, Quaternion.identity);
            segment.GetComponent<CylinderSegment>().object1 = first.transform;
            segment.GetComponent<CylinderSegment>().object2 = second.transform;
            segment.transform.SetParent(transform);
            cylinderSegments.Add(segment);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
