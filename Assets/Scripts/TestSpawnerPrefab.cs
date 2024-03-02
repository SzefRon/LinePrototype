using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnerPrefab : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject t1;
    [SerializeField] GameObject t2;
    int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        idx++;

        if(idx == 100)
        {
            idx =0;
            var newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = transform.position;
            newEnemy.GetComponent<EnemyScript>().followTargets = new()
            {
                t1,
                t2
            };
        }
    }
}
