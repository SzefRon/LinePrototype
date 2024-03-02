using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    [SerializeField] public GameObject ropeSegmentPrefab;
    [SerializeField] public GameObject ropeSegmentPrefab2;
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject end;
    [SerializeField] public uint segments;

    public List<GameObject> ballJoints = new();
    public List<GameObject> smallSegments = new();    

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(3, 6);
        Physics.IgnoreLayerCollision(6, 3);
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
            segment.GetComponent<SmallSegment>().object1 = first.transform;
            segment.GetComponent<SmallSegment>().object2 = second.transform;
            segment.GetComponent<SmallSegment>().id = (uint)smallSegments.Count + 1;
            segment.transform.SetParent(transform);
            smallSegments.Add(segment);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
