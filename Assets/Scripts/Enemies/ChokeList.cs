using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChokeList
{
    public static List<GameObject> chokedObjects = new();

    public static void DealDmgToObjectsInList()
    {
        foreach (GameObject obj in chokedObjects) 
        {
            var health = obj.GetComponent<HealthComponent>();   
            float dmg = health.MaxHealthFraction(4);
            health.TakeDamage(dmg);
        }
    }
}
