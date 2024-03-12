using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectComponent : MonoBehaviour
{
    private HealthComponent healthComponent;
    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void ApplyRopeEffect(SegmentUpgrades upgrade)
    {
        switch (upgrade)
        {
            case SegmentUpgrades.Fire:
                Debug.Log("Fire");
                float dmg = healthComponent.MaxHealthFraction(10);
                healthComponent.TakeDamageOverTime(dmg, 1.0f, 5);
                break;
            case SegmentUpgrades.Electricity:
                Debug.Log("Electricity");
                break;
            case SegmentUpgrades.Laser:
                Debug.Log("Laser");
                break;
            case SegmentUpgrades.Shredder:
                Debug.Log("Shredder");
                break;
            case SegmentUpgrades.Mirror:
                Debug.Log("Mirror");
                break;
            case SegmentUpgrades.Explosive:
                Debug.Log("Explosive");
                break;
            case SegmentUpgrades.Lifesteal:
                Debug.Log("Lifesteal");
                break;
            case SegmentUpgrades.Shield:
                Debug.Log("Shield");
                break;
            case SegmentUpgrades.Slime:
                Debug.Log("Slime");
                break;
            default:
                Debug.Log("None");
                break;
        }
    }
}
