using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeEffectsModifier : MonoBehaviour
{
    public RopeGenerator data;
    public RopeSelectionManager manager;

    [SerializeField] public GameObject bladesPrefab;
    [SerializeField] public GameObject flamePrefab;
    [SerializeField] public GameObject shockPrefab;
    [SerializeField] public GameObject mirrorPrefab;

    void Start()
    {
        data = GetComponent<RopeGenerator>();
        manager = GetComponent<RopeSelectionManager>();
    }

    void AddEffect(GameObject prefab, SegmentUpgrades segmentType)
    {
        // Dla wszystkich obecnie wybranych segmentow
        foreach (var a in manager.selectedSmallSegments)
        {
            a.transform.GetComponent<SmallSegment>().segmentType = segmentType;
            foreach (var b in a.GetComponent<ModifiableSegment>().attachmentPoints)
            {
                // Wchodzi jezeli obecnie jest cos doczepione do tego punku
                if (b.transform.childCount > 0)
                {
                    // Niszczy obecne efekty
                    for (int i = 0; i < b.transform.childCount; i++)
                    {
                        DestroyImmediate(b.transform.GetChild(i).gameObject);
                    }
                }

                GameObject newEffect = Instantiate(prefab);
                newEffect.transform.SetParent(b.transform);
                newEffect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    void ClearEffects()
    {
        // Dla wszystkich obecnie wybranych segmentow
        foreach (var a in manager.selectedSmallSegments)
        {
            a.transform.GetComponent<SmallSegment>().segmentType = SegmentUpgrades.None;
            foreach (var b in a.GetComponent<ModifiableSegment>().attachmentPoints)
            {
                // Wchodzi jezeli obecnie jest cos doczepione do tego punku
                if (b.transform.childCount > 0)
                {
                    // Niszczy obecne efekty
                    for (int i = 0; i < b.transform.childCount; i++)
                    {
                        DestroyImmediate(b.transform.GetChild(i).gameObject);
                    }
                }
            }
        }
    }

    void Update()
    {
        // Dodaje ostrza
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddEffect(bladesPrefab, SegmentUpgrades.Laser);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddEffect(flamePrefab, SegmentUpgrades.Fire);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddEffect(shockPrefab, SegmentUpgrades.Electricity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddEffect(mirrorPrefab, SegmentUpgrades.Mirror);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ClearEffects();
        }
    }
}
