using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int baseChance;

    public void Drop(Vector3 position, int rate)
    {
        int chance = baseChance * rate;

        //Drop
        if (chance > 100)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject loot = Instantiate(prefabs[randomIndex]);
            loot.transform.position = new Vector3(position.x, 0.0f, position.z);
        }
        else
        {
            int rng = Random.Range(0, 100);
            {
                if (rng <= chance)
                {
                    int randomIndex = Random.Range(0, prefabs.Length);
                    GameObject loot = Instantiate(prefabs[randomIndex]);
                    loot.transform.position = new Vector3(position.x, 0.0f, position.z);
                }
            }
        }
    }
}
