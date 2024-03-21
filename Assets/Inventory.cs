using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<SegmentUpgrades, int> inventory = new();

    [SerializeField] public TextMeshProUGUI textMeshProUGUI;

    private void Update()
    {
        string text = string.Empty;

        if (inventory.Count > 0)
        {
            foreach (KeyValuePair<SegmentUpgrades, int> keyValuePair in inventory)
            {
                text += $"{keyValuePair.Key}: {keyValuePair.Value}\n";
            }

            textMeshProUGUI.text = text;
        }
    }
}
