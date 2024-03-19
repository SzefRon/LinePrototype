using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeModifier : MonoBehaviour
{
    [SerializeField] RopeSelectionManager ropeSelectionManager;
    
    [SerializeField] GameObject minonEffect;
    [SerializeField] SegmentUpgrades minionType;

    // Start is called before the first frame update
    void Start()
    {
        ropeSelectionManager = GetComponent<RopeSelectionManager>();    
    }

    void AddUpgrade(GameObject prefab, SegmentUpgrades type)
    {
        foreach (var segment in ropeSelectionManager.selectedSmallSegments)
        {
            var attachmentPoints = segment.GetComponent<ModifiableSegment>().attachmentPoints;

            for (int i = 0; i < attachmentPoints.Length; i++)
            {
                for(int j = 0; j < attachmentPoints[i].transform.childCount; j++)
                {
                    Destroy(attachmentPoints[i].transform.GetChild(j).gameObject);  

                }

                GameObject effect = Instantiate(prefab);
                effect.transform.parent = attachmentPoints[i].transform;
                effect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                effect.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                
            }
        }
    }

    void ClearUpgrades()
    {
        foreach (var segment in ropeSelectionManager.selectedSmallSegments)
        {
            var attachmentPoints = segment.GetComponent<ModifiableSegment>().attachmentPoints;

            for (int i = 0; i < attachmentPoints.Length; i++)
            {
                for (int j = 0; j < attachmentPoints[i].transform.childCount; j++)
                {
                    Destroy(attachmentPoints[i].transform.GetChild(j).gameObject);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            AddUpgrade(minonEffect, minionType);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ClearUpgrades();
        }
    }
}
