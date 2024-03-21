using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        // Remove null objects from the list
        enemies.RemoveAll(enemy => enemy == null);

        if (!enemies.Any())
        {
            gameObject.SetActive(false);
        }
    }
}
