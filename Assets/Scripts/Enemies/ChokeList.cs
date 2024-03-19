using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChokeList
{
    public static List<GameObject> chokedObjects = new();

    public static void DealDmgToObjectsInList()
    {
        for(int i = 0; i < chokedObjects.Count; i++)
        {
            var obj = chokedObjects[i];
            var health = obj.GetComponent<HealthComponent>();
            float dmg = health.MaxHealthFraction(2 + (chokedObjects.Count - 1));
            health.ForceDamage(dmg);
        }
    }
}
