using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEffectsModifier : MonoBehaviour
{
    public RopeGenerator data;
    public RopeSelectionManager manager;

    [SerializeField] public GameObject bladesPrefab;

    void Start()
    {
        data = GetComponent<RopeGenerator>();
        manager = GetComponent<RopeSelectionManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            print("AA");
            foreach(var a in manager.selectedSmallSegments) 
            {
                print("BB");
                foreach (var b in a.GetComponent<ModifiableSegment>().attachmentPoints)
                {
                    print("CC");
                    // Wchodzi jezeli obecnie jest cos doczepione do tego punku
                    if (b.transform.childCount > 0)
                    {
                        // Niszczy obecne efekty
                        for(int i = 0; i < b.transform.childCount; i++)
                        {
                            DestroyImmediate(b.transform.GetChild(i).gameObject);
                        }
                    }

                    GameObject effect = Instantiate(bladesPrefab);
                    effect.transform.SetParent(b.transform);
                    effect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                    effect.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                    effect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    
                }
            }
        }
    }
}
