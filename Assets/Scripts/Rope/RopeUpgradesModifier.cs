using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using UnityEngine;

public class OnUpgradeChangedArgs : EventArgs
{
    private int index;
    private SegmentUpgrades upgrade;
    public OnUpgradeChangedArgs(int index, SegmentUpgrades upgrade)
    {
        this.index = index;
        this.upgrade = upgrade; 
    }
    public int Index { get { return index; } }
    public SegmentUpgrades Upgrade { get {  return upgrade; } }
}

public class RopeUpgradesModifier : MonoBehaviour
{
    public RopeGenerator data;
    public RopeSelectionManager manager;

    [SerializeField] public GameObject bladesPrefab;
    [SerializeField] public GameObject flamePrefab;
    [SerializeField] public GameObject shockPrefab;
    [SerializeField] public GameObject mirrorPrefab;

    public event EventHandler<OnUpgradeChangedArgs> UpgradeChanged;

    public virtual void OnUpgradeChanged(OnUpgradeChangedArgs e)
    {
        EventHandler<OnUpgradeChangedArgs> handler = UpgradeChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }

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

        if(manager.selectedBigSegment > -1)
        {
            OnUpgradeChangedArgs onUpgradeChangedArgs = new OnUpgradeChangedArgs(manager.selectedBigSegment, segmentType);
            OnUpgradeChanged(onUpgradeChangedArgs);
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

        OnUpgradeChangedArgs onUpgradeChangedArgs = new OnUpgradeChangedArgs(manager.selectedBigSegment, SegmentUpgrades.None);
        OnUpgradeChanged(onUpgradeChangedArgs);
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
