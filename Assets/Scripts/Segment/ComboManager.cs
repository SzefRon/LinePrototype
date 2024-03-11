using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnComboCheckedArgs : EventArgs
{
    private int currentSegment;
    private SegmentUpgrades combo1;
    private SegmentUpgrades combo2;

    public SegmentUpgrades Combo1 { get => combo1; set => combo1 = value; }
    public SegmentUpgrades Combo2 { get => combo2; set => combo2 = value; }
    public int CurrentSegment { get => currentSegment; set => currentSegment = value; }

    public OnComboCheckedArgs(int currentSegment, SegmentUpgrades combo1, SegmentUpgrades combo2)
    {
        this.currentSegment = currentSegment;
        this.combo1 = combo1;
        this.combo2 = combo2;
    }   
}


public class ComboManager : MonoBehaviour
{
    public Dictionary<(SegmentUpgrades, SegmentUpgrades), SegmentUpgrades> comboDictionary = new Dictionary<(SegmentUpgrades, SegmentUpgrades), SegmentUpgrades>
    {
        // Double ups
        {(SegmentUpgrades.Fire, SegmentUpgrades.Fire), (SegmentUpgrades.DoubleFire) },
        {(SegmentUpgrades.Electricity, SegmentUpgrades.Electricity), (SegmentUpgrades.DoubleElectricity) },
        {(SegmentUpgrades.Laser, SegmentUpgrades.Laser), (SegmentUpgrades.DoubleLaser) },
        {(SegmentUpgrades.Shredder, SegmentUpgrades.Shredder), (SegmentUpgrades.DoubleShredder) },
        {(SegmentUpgrades.Mirror, SegmentUpgrades.Mirror), (SegmentUpgrades.DoubleMirror) },
        {(SegmentUpgrades.Explosive, SegmentUpgrades.Explosive), (SegmentUpgrades.DoubleExplosive) },
        {(SegmentUpgrades.Lifesteal, SegmentUpgrades.Lifesteal), (SegmentUpgrades.DoubleLifesteal) },
        {(SegmentUpgrades.Slime, SegmentUpgrades.Slime), (SegmentUpgrades.DoubleSlime) },
        
        // Combos - w jedna
        {(SegmentUpgrades.Fire, SegmentUpgrades.Shield), (SegmentUpgrades.FireShield) },
        {(SegmentUpgrades.Electricity, SegmentUpgrades.Shield), (SegmentUpgrades.ElectricityShield) },
        {(SegmentUpgrades.Explosive, SegmentUpgrades.Shield), (SegmentUpgrades.ExplosiveShield) },
        {(SegmentUpgrades.Electricity, SegmentUpgrades.Shredder), (SegmentUpgrades.ElectricityShredder) },
        {(SegmentUpgrades.Laser, SegmentUpgrades.Shredder), (SegmentUpgrades.LaserShredder) },
        {(SegmentUpgrades.Fire, SegmentUpgrades.Mirror), (SegmentUpgrades.FireMirror) },
        {(SegmentUpgrades.Electricity, SegmentUpgrades.Mirror), (SegmentUpgrades.ElectricityMirror) },
        {(SegmentUpgrades.Explosive, SegmentUpgrades.Mirror), (SegmentUpgrades.ExplosiveMirror) },
        {(SegmentUpgrades.Shredder, SegmentUpgrades.Lifesteal), (SegmentUpgrades.ShredderLifesteal) },

        // Combos - w druga
        {(SegmentUpgrades.Shield, SegmentUpgrades.Fire), (SegmentUpgrades.FireShield) },
        {(SegmentUpgrades.Shield, SegmentUpgrades.Electricity), (SegmentUpgrades.ElectricityShield) },
        {(SegmentUpgrades.Shield, SegmentUpgrades.Explosive), (SegmentUpgrades.ExplosiveShield) },
        {(SegmentUpgrades.Shredder, SegmentUpgrades.Electricity), (SegmentUpgrades.ElectricityShredder) },
        {(SegmentUpgrades.Shredder, SegmentUpgrades.Laser), (SegmentUpgrades.LaserShredder) },
        {(SegmentUpgrades.Mirror, SegmentUpgrades.Fire), (SegmentUpgrades.FireMirror) },
        {(SegmentUpgrades.Mirror, SegmentUpgrades.Electricity), (SegmentUpgrades.ElectricityMirror) },
        {(SegmentUpgrades.Mirror, SegmentUpgrades.Explosive), (SegmentUpgrades.ExplosiveMirror) },
        {(SegmentUpgrades.Lifesteal, SegmentUpgrades.Shredder), (SegmentUpgrades.ShredderLifesteal) },
    };

    RopeGenerator ropeGenerator;
    RopeUpgradesModifier ropeUpgradesModifier;
    RopeSelectionManager selectionManager;

    public SegmentUpgrades[] segments;

    // Start is called before the first frame update
    void Start()
    {
        ropeGenerator = GetComponent<RopeGenerator>();
        ropeUpgradesModifier = GetComponent<RopeUpgradesModifier>();
        selectionManager = GetComponent<RopeSelectionManager>();

        segments = new SegmentUpgrades[ropeGenerator.segmentNum];
        for(int i = 0; i < segments.Length; i++)
        {
            segments[i] = SegmentUpgrades.None;
        }
        ropeUpgradesModifier.UpgradeChanged += CheckForCombos;
    }

    public event EventHandler<OnComboCheckedArgs> ComboChanged;

    public virtual void OnComboChecked(OnComboCheckedArgs e)
    {
        EventHandler<OnComboCheckedArgs> handler = ComboChanged;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    void CheckForCombos(object sender, OnUpgradeChangedArgs e)
    {
        segments[e.Index] = e.Upgrade; 
        int currentIndex = e.Index;
        int previous = currentIndex - 1;
        int next = currentIndex + 1;

        SegmentUpgrades previousUpgrade = SegmentUpgrades.None;
        SegmentUpgrades nextUpgrade = SegmentUpgrades.None;

        SegmentUpgrades combo1 = SegmentUpgrades.None;
        SegmentUpgrades combo2 = SegmentUpgrades.None;

        (SegmentUpgrades, SegmentUpgrades) possibleCombo1;
        (SegmentUpgrades, SegmentUpgrades) possibleCombo2;

        if (previous != -1)
        {
            previousUpgrade = segments[previous];
        }

        if(next < segments.Length) 
        {
            nextUpgrade = segments[next];
        }

        if(e.Upgrade != SegmentUpgrades.None) 
        {
            if(previousUpgrade != SegmentUpgrades.None)
            {
                possibleCombo1 = (e.Upgrade, previousUpgrade);
                if (comboDictionary.ContainsKey(possibleCombo1))
                {
                    combo1 = comboDictionary[possibleCombo1];
                }
            }

            if(nextUpgrade != SegmentUpgrades.None)
            {
                possibleCombo2 = (e.Upgrade, nextUpgrade);
                if(comboDictionary.ContainsKey(possibleCombo2))
                {
                    combo2 = comboDictionary[possibleCombo2];
                }
            }
        }

        OnComboCheckedArgs args = new(e.Index, combo1, combo2);
        OnComboChecked(args);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
