using System.Collections;
using UnityEngine;

public class TestSpawnerPrefab : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject t1;
    [SerializeField] GameObject t2;
    [SerializeField] float spawnInterval;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {


        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<EnemyScript>().followTargets = new(new[] { t1, t2 });
            enemy.transform.parent = transform;
            enemy.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
