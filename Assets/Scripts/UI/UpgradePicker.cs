using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        ComboManager comboManager = FindObjectOfType<ComboManager>();

        comboManager.ComboChanged += RecieveMessage;

        textCombo1 = buttonCombo1.transform.GetChild(0).GetComponent<TMP_Text>();
        textCombo2 = buttonCombo2.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    void RecieveMessage(object sender, OnComboCheckedArgs e)
    {
        if(e.Combo1 == SegmentUpgrades.None)
        {
            textCombo1.text = "No combo";
        }
        else
        {
            textCombo1.text = e.Combo1.ToString();
        }

        if (e.Combo1 == SegmentUpgrades.None)
        {
            textCombo2.text = "No combo";
        }
        else
        {
            textCombo2.text = e.Combo2.ToString();
        }
    }

    public void AddNone()
    {
        print("Add None");
    }

    public void AddFire()
    {
        print("Add Fire");
    }

    public void AddElectricity()
    {
        print("Add Electricity");
    }

    public void AddLaser()
    {
        print("Add Laser");
    }

    public void AddShredder()
    {
        print("Add Shredder");
    }

    public void AddMirror()
    {
        print("Add Mirror");
    }

    public void AddExplosive()
    {
        print("Add Explosive");
    }

    public void AddVampirism()
    {
        print("Add Vampirism");
    }

    public void AddShield()
    {
        print("Add Shield");
    }

    public void AddSlime()
    {
        print("Add Slime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
