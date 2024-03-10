using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePicker : MonoBehaviour
{
    [SerializeField] Button buttonCombo1;
    [SerializeField] Button buttonCombo2;

    [SerializeField] TMP_Text textCombo1;
    [SerializeField] TMP_Text textCombo2;

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
        textCombo1.text = e.Combo1.ToString();
        textCombo2.text = e.Combo2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
