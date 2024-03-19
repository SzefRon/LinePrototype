using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePickedArgs : EventArgs
{
    private SegmentUpgrades upgrade;
    public SegmentUpgrades Upgrade { get => upgrade; }
    public UpgradePickedArgs(SegmentUpgrades upgrade)
    {
        this.upgrade = upgrade;
    }
}

public class ComboPickedArgs : EventArgs
{
    private SegmentUpgrades upgrade;
    private int a;
    private int b;

    public SegmentUpgrades Upgrade { get => upgrade; }
    public int A { get => a; }
    public int B { get => b; }

    public ComboPickedArgs(int a, int b, SegmentUpgrades upgrade)
    {
        this.a = a; 
        this.b = b;
        this.upgrade = upgrade;
    }
}

public class UpgradePicker : MonoBehaviour
{
    [SerializeField] Button buttonCombo1;
    [SerializeField] Button buttonCombo2;

    [SerializeField] Button buttonNone;
    [SerializeField] Button buttonFire;
    [SerializeField] Button buttonElectricity;
    [SerializeField] Button buttonLaser;
    [SerializeField] Button buttonShredder;
    [SerializeField] Button buttonMirror;
    [SerializeField] Button buttonBoom;
    [SerializeField] Button buttonVampirism;
    [SerializeField] Button buttonShield;
    [SerializeField] Button buttonSlime;

    TMP_Text textCombo1;
    TMP_Text textCombo2;

    public SegmentUpgrades comboWithPreviousSegment = SegmentUpgrades.None;
    public SegmentUpgrades comboWithNextSegment = SegmentUpgrades.None;

    RopeSelectionManager ropeSelectionManager;
  
/*    // Start is called before the first frame update
    void Start()
    {
        comboManager = FindObjectOfType<ComboManager>();
        comboManager.ComboChanged += UpdateComboSelection;

        ropeSelectionManager = FindObjectOfType<RopeSelectionManager>();
        modifier = FindObjectOfType<RopeUpgradesModifier>();

        textCombo1 = buttonCombo1.transform.GetChild(0).GetComponent<TMP_Text>();
        textCombo2 = buttonCombo2.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public event EventHandler<UpgradePickedArgs> UpgradePicked;
    public virtual void OnUpgradePicked(UpgradePickedArgs e)
    {
        EventHandler<UpgradePickedArgs> handler = UpgradePicked;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<ComboPickedArgs> ComboPicked;
    public virtual void OnComboPicked(ComboPickedArgs e) 
    {
        EventHandler<ComboPickedArgs> handler = ComboPicked;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    void UpdateComboSelection(object sender, OnComboCheckedArgs e)
    {
        comboWithPreviousSegment = e.ComboWithPreviousSegment;
        comboWithNextSegment = e.ComboWithNextSegment;
        if (e.ComboWithPreviousSegment == SegmentUpgrades.None)
        {
            textCombo1.text = "No combo";
        }
        else
        {
            textCombo1.text = e.ComboWithPreviousSegment.ToString();
        }

        if (e.ComboWithNextSegment == SegmentUpgrades.None)
        {
            textCombo2.text = "No combo";
        }
        else
        {
            textCombo2.text = e.ComboWithNextSegment.ToString();
        }
    }

    public void ApplyComboWithPreviousSegment()
    {
        if(comboWithPreviousSegment != SegmentUpgrades.None) 
        {
            int currentSegment = ropeSelectionManager.selectedBigSegment;
            int previousSegment = currentSegment - 1;

            comboManager.segments[previousSegment] = comboWithPreviousSegment;
            comboManager.segments[currentSegment] = comboWithPreviousSegment;

            OnComboPicked(new ComboPickedArgs(currentSegment, previousSegment, comboWithPreviousSegment));

            print(comboWithPreviousSegment);
        }
    }

    public void ApplyComboWithNextSegment()
    {
        if(comboWithNextSegment != SegmentUpgrades.None)
        {
            int currentSegment = ropeSelectionManager.selectedBigSegment;
            int nextSegment = currentSegment + 1;

            comboManager.segments[nextSegment] = comboWithNextSegment;
            comboManager.segments[currentSegment] = comboWithNextSegment;

            OnComboPicked(new ComboPickedArgs(currentSegment, nextSegment, comboWithNextSegment));

            print(comboWithNextSegment);
        }
    }


    public void AddNone()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.None));
    }

    public void AddFire()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Fire));
    }

    public void AddElectricity()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Electricity));
    }

    public void AddLaser()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Laser));
    }

    public void AddShredder()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Shredder));
    }

    public void AddMirror()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Mirror));
    }

    public void AddExplosive()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Explosive));
    }

    public void AddVampirism()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Lifesteal));
    }

    public void AddShield()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Shield));
    }

    public void AddSlime()
    {
        OnUpgradePicked(new UpgradePickedArgs(SegmentUpgrades.Slime));
    }*/
}
