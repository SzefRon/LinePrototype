using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    [SerializeField] public SegmentUpgrades type;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log($"{type} upgrade collected!");
            Destroy(gameObject);

            var inventory = FindAnyObjectByType<Inventory>();
            if (inventory.inventory.ContainsKey(type))
            {
                inventory.inventory[type]++;
            }
            else
            {
                inventory.inventory.Add(type, 1);   
            }
        }
    }
}
