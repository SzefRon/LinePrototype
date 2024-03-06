using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public List<GameObject> followTargets;
    [SerializeField] private float followSpeed;
    [SerializeField] private float followRange;
    private uint collisions = 0;
    
    [SerializeField] public int collisionsToDeath;

    [SerializeField] public int hp;
    [SerializeField] public GameObject bloodSplashPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Segments")) {
            collisions++;
        }

        if (other.transform.tag == "BladeEffect")
        {
            hp--;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Equals("Segments")) {
            collisions--;
        }
    }

    void FixedUpdate()
    {
        if (collisions >= collisionsToDeath) {
            Destroy(gameObject, 1);
        }

        if (hp == 0)
        {
            var effect = Instantiate(bloodSplashPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2);
            Destroy(gameObject, 1);
            
        }

        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 directionToClosest = Vector3.zero;
        foreach (var target in followTargets) {
            Vector3 direction = target.transform.position - transform.position;
            float distance = direction.magnitude;
            if (distance < closestDistance) {
                closestDistance = distance;
                closestTarget = target;
                directionToClosest = new(direction.x, 0, direction.z);
            }
        }

        Debug.Log(collisions);
        if (closestTarget != null && closestDistance < followRange
            && collisions <= 4) {
            transform.position += followSpeed * Time.fixedDeltaTime * directionToClosest.normalized;
        }

        
    }
}
