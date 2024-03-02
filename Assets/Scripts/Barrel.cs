using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] public GameObject flamePrefab;
    [SerializeField] public GameObject objectInBarrel;

    [SerializeField] public GameObject[] flameSpots;

    [SerializeField] public string weakLayer;
    [SerializeField] public int hp;

    private int idx = 0;
    
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.transform.tag);
        if(collision.transform.tag == "FlameEffect")
        {
            print("AAAAA");
            if(idx < flameSpots.Length)
            {
                GameObject newFlames = GameObject.Instantiate(flamePrefab);
                newFlames.transform.SetParent(flameSpots[idx].transform);
                newFlames.transform.localPosition = Vector3.zero;
                idx++;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(idx < 3)
        {
            hp -= idx;
        }
        else
        {
            hp -= flameSpots.Length;
        }

        if(hp < 0)
        {
            Destroy(gameObject);
        }
    }
}
