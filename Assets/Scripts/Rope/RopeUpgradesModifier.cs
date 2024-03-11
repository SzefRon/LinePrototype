using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public SegmentUpgrades Upgrade { get { return upgrade; } }
}

[Serializable]
public struct UpgradeToPrefabDictPair
{
    public SegmentUpgrades Upgrade;
    public GameObject Prefab;
}

public class RopeUpgradesModifier : MonoBehaviour
{
    [SerializeField] public UpgradeToPrefabDictPair[] pairs;
    public RopeGenerator data;
    public RopeSelectionManager manager;

    private Dictionary<SegmentUpgrades, GameObject> upgradeToPrefabDict;

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
        upgradeToPrefabDict = new();

        for (int i = 0; i < pairs.Length; i++)
        {
            upgradeToPrefabDict.Add(pairs[i].Upgrade, pairs[i].Prefab);
        }

        data = GetComponent<RopeGenerator>();
        manager = GetComponent<RopeSelectionManager>();
        UpgradePicker picker = FindAnyObjectByType<UpgradePicker>();
        picker.UpgradePicked += GetPickedUpgrade;
        picker.ComboPicked += GePickedCombo;
    }

    void AddEffect(GameObject prefab, SegmentUpgrades segmentType)
    {
        // Dla wszystkich obecnie wybranych ma³ych segmentow
        foreach (var a in manager.selectedSmallSegments)
        {
            // Dla obu stron ma³ego segmentu
            foreach (var b in a.GetComponent<ModifiableSegment>().attachmentPoints)
            {
                // Wchodzi jezeli obecnie jest cos doczepione do tego punku
                if (b.transform.childCount > 0)
                {
                    // Niszczy obecne doczepione efekty
                    for (int i = 0; i < b.transform.childCount; i++)
                    {
                        DestroyImmediate(b.transform.GetChild(i).gameObject);
                    }
                }

                // Instancojune nowy efekt
                GameObject newEffect = Instantiate(prefab);
                newEffect.transform.SetParent(b.transform);
                newEffect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            a.transform.GetComponent<SmallSegment>().segmentType = segmentType;
        }

        if (manager.selectedBigSegment > -1)
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

    void GetPickedUpgrade(object sender, UpgradePickedArgs e)
        {
        if (e.Upgrade == SegmentUpgrades.None)
        {
            ClearEffects();
        }
        else
        {
            AddEffect(upgradeToPrefabDict[e.Upgrade], e.Upgrade);
        }
    }

    void GePickedCombo(object sender, ComboPickedArgs e)
    {
        int beginning = e.B * manager.segInSeg;
        int end = (e.B + 1) * manager.segInSeg;

        List<GameObject> tmp = new();

        for (int i = beginning; i < end; i++)
        {
            tmp.Add(data.smallSegments[i]);
        }

        beginning = e.A * manager.segInSeg;
        end = (e.A + 1) * manager.segInSeg;

        for (int i = beginning; i < end; i++)
        {
            tmp.Add(data.smallSegments[i]);
        }

        foreach (var a in tmp)
        {
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

                // Instancojune nowy efekt
                GameObject newEffect = Instantiate(upgradeToPrefabDict[e.Upgrade]);
                newEffect.transform.SetParent(b.transform);
                newEffect.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                newEffect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            a.transform.GetComponent<SmallSegment>().segmentType = e.Upgrade;
        }
    }
}
