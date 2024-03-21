using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject morphPrefab;
    [SerializeField] public List<GameObject> followTargets;
    [SerializeField] private float followSpeed;
    [SerializeField] private float followRange;
    private uint collisions = 0;
    private ChokeChecker chokeChecker;

    [SerializeField] public int collisionsToDeath;
    [SerializeField] public GameObject bloodSplashPrefab;
    public int dropRate;
    public int spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        followTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        chokeChecker = GetComponent<ChokeChecker>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Minion"))
        {
            if (!chokeChecker.isChoked)
            {
                var healthComponent = other.gameObject.GetComponent<HealthComponent>();
                healthComponent.TakeDamage(5.0f);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Segments"))
        {
            collisions++;
        }
        //Debug.Log(collisions);
    }

    private void OnCollisionExit(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Segments"))
        {
            collisions--;
        }
    }

    public void Drop()
    {
        FindAnyObjectByType<DropManager>().Drop(transform.position, dropRate);
    }

    void FixedUpdate()
    {
        /*if(collisions >= collisionsToDeath) 
        {
            Destroy(gameObject, 1);
        }*/

        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 directionToClosest = Vector3.zero;
        foreach (var target in followTargets)
        {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
                directionToClosest = new(direction.x, 0, direction.z);
            }
        }

        if (closestTarget != null && closestDistance < followRange
            && collisions <= 4)
        {
            transform.position += followSpeed * Time.fixedDeltaTime * directionToClosest.normalized;
        }
    }

    public void Morph()
    {
        var inventory = FindAnyObjectByType<Inventory>();
        if (inventory.inventory.ContainsKey(SegmentUpgrades.Minion))
        {
            int a = inventory.inventory[SegmentUpgrades.Minion];
            Debug.Log(a);

            if (a == 0)
            {

            }
            else if (a > 0 && a < spawnRate + 1)
            {
                Instantiate(morphPrefab, transform.position - Vector3.down * 0.5f, transform.rotation);
            }
            else if( a > spawnRate)
            {
                int amount = a / spawnRate;
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(morphPrefab, transform.position - Vector3.down * 0.5f, transform.rotation);
                }
            }
        }
    }
}
