using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RopeGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] public GameObject ballJointPrefab;
    [SerializeField] public GameObject smallSegmentPrefab;

    [Header("Players")]
    [SerializeField] public GameObject start;
    [SerializeField] public GameObject end;

    [Header("Segment Configuration")]
    [SerializeField] public int segmentNum;
    [SerializeField] public uint smallSegmentsNum;

    [Header("Rope Fragments")]
    public List<GameObject> ballJoints = new();
    public List<GameObject> smallSegments = new();

    void Generate()
    {
        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(3, 6);
        Physics.IgnoreLayerCollision(6, 3);
        Rigidbody startRb = start.GetComponent<Rigidbody>();
        Rigidbody endRb = end.GetComponent<Rigidbody>();

        Vector3 p1 = start.transform.position;
        Vector3 p2 = end.transform.position;

        Vector3 direction = p2 - p1;
        uint div = smallSegmentsNum + 1;
        Vector3 offset = direction / div;

        GameObject lastObj = start;
        for (uint i = 0; i <= div; i++)
        {
            GameObject segmentObject = Instantiate(ballJointPrefab, p1 + offset * i, Quaternion.identity);
            segmentObject.transform.name = $"Joint {i}";
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

        for (int i = 0; i < ballJoints.Count - 1; i++)
        {
            GameObject first = ballJoints[i];
            GameObject second = ballJoints[i + 1];
            Vector3 position = Vector3.Lerp(first.transform.position, second.transform.position, 0.5f);

            GameObject segment = Instantiate(smallSegmentPrefab, position, Quaternion.identity);
            segment.GetComponent<SmallSegment>().object1 = first.transform;
            segment.GetComponent<SmallSegment>().object2 = second.transform;
            segment.GetComponent<SmallSegment>().id = (uint)i;
            segment.transform.name = $"Small Rope Segment {i}";
            segment.transform.SetParent(transform);
            smallSegments.Add(segment);
        }
    }

    void Start()
    {
        Assert.IsTrue((smallSegmentsNum % segmentNum) == 0);
        Generate();
    }
}
