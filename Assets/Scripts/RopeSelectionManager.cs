using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class RopeSelectionManager : MonoBehaviour
{
    [SerializeField] public RopeGenerator data;
    [SerializeField] public Material selectedMaterial;
    [SerializeField] public Material defaultMaterial;
    [SerializeField] public int bigSegmentNum;

    public int previousSelectedBigSegment = -1;
    public int selectedBigSegment = -1;
    private int segInSeg;

    public List<GameObject> selectedSmallSegments = new();

    // Start is called before the first frame update
    void Start()
    {
        data = transform.GetComponent<RopeGenerator>();      
        Assert.IsTrue((data.segments % bigSegmentNum) == 0);
        segInSeg = (int)data.segments / bigSegmentNum;
    }

    // Update is called once per frame
    void Update()
    {
        previousSelectedBigSegment = selectedBigSegment;
        if (Input.GetKey(KeyCode.Escape)) 
        {
            selectedBigSegment = -1;
            
            for (int i = 0; i < data.smallSegments.Count; i++)
            {
                data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectedSmallSegments.Clear();
            selectedBigSegment++;

            if(selectedBigSegment == bigSegmentNum)
            {
                selectedBigSegment = 0;
            }
        }

        if (previousSelectedBigSegment != selectedBigSegment)
        {
            if (selectedBigSegment != -1)
            {
                if (selectedBigSegment == 0)
                {
                    int begining = (bigSegmentNum - 1) * segInSeg;
                    int end = (bigSegmentNum) * segInSeg;

                    for (int i = begining; i < end; i++)
                    {
                        data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = defaultMaterial;
                    }

                    begining = selectedBigSegment * segInSeg;
                    end = (selectedBigSegment + 1) * segInSeg;

                    for (int i = begining; i < end; i++)
                    {
                        data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = selectedMaterial;
                        selectedSmallSegments.Add(data.smallSegments[i]);
                    }

                    //Logowanie
                    foreach (var a in selectedSmallSegments)
                    {
                        print(a.GetComponent<SmallSegment>().id);
                    }
                }
                else
                {
                    int begining = (selectedBigSegment - 1) * segInSeg;
                    int end = (selectedBigSegment - 1 + 1) * segInSeg;

                    for (int i = begining; i < end; i++)
                    {
                        data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = defaultMaterial;
                    }

                    begining = selectedBigSegment * segInSeg;
                    end = (selectedBigSegment + 1) * segInSeg;

                    for (int i = begining; i < end; i++)
                    {
                        data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = selectedMaterial;
                        selectedSmallSegments.Add(data.smallSegments[i]);
                    }

                    //Logowanie
                    foreach (var a in selectedSmallSegments)
                    {
                        print(a.GetComponent<SmallSegment>().id);
                    }
                }
            }
        }
    }
}
