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
        // Odznaczenie wszystkiego
        if (Input.GetKey(KeyCode.Escape)) 
        {
            selectedSmallSegments.Clear();
            selectedBigSegment = -1;
            
            for (int i = 0; i < data.smallSegments.Count; i++)
            {
                data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = defaultMaterial;
            }
        }

        // Przesuniecie od gracza 1 do gracza 2. W domysle bedzie to udostepnione graczowi 1
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            selectedSmallSegments.Clear();
            selectedBigSegment++;

            if(selectedBigSegment == bigSegmentNum)
            {
                selectedBigSegment = 0;
            }
        }


        // Przesuniecie od gracza 2 do gracza 1. Dla gracza 2
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            selectedSmallSegments.Clear();
            if((selectedBigSegment == -1) || (selectedBigSegment == 0))
            {
                selectedBigSegment = bigSegmentNum -1;
            }
            else
            {
                selectedBigSegment--;

            }
        }

        // Wchodz jesli jest cos wybrane
        if (selectedBigSegment != -1) 
        {
            // Wchodzi jesli wybor sie zmienil
            if (previousSelectedBigSegment != selectedBigSegment)
            {
                int beginning = 0;
                int end = 0;
                //Odkolorowuje poprzednie
                //Wchodzi jestli poprzednie to bylo zaznaczenie a nie brak zaznaczenia
                if (previousSelectedBigSegment != -1)
                {
                    beginning = previousSelectedBigSegment * segInSeg;
                    end = (previousSelectedBigSegment + 1) * segInSeg;

                    for (int i = beginning; i < end; i++)
                    {
                        data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = defaultMaterial;
                    }
                }

                //Zakolorowuje nowe
                beginning = selectedBigSegment * segInSeg;
                end = (selectedBigSegment + 1) * segInSeg;

                for (int i = beginning; i < end; i++)
                {
                    data.smallSegments[i].transform.GetComponent<MeshRenderer>().material = selectedMaterial;
                    selectedSmallSegments.Add(data.smallSegments[i]);
                }
            }
        }
    }
}
