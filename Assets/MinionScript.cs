using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.1f;
    private HealthComponent healthComponent;
    Transform followTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        StartCoroutine(SetTarget());
    }

    IEnumerator SetTarget()
    {
        yield return new WaitForSeconds(1.0f);
        var enemies = GameObject.FindGameObjectsWithTag("Monster");
        float minDistance = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < 50.0f && distance < minDistance)
            {
                minDistance = distance;
                followTarget = enemy.transform;
            }
        }
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(5.0f);
        healthComponent.TakeDamage(healthComponent.MaxHealthFraction(1.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget == null)
        {
            return;
        }
        Vector3 direction = (followTarget.position - transform.position).normalized;
        transform.position += direction * followSpeed;
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            var otherHealthComponent = other.gameObject.GetComponent<HealthComponent>();
            otherHealthComponent.TakeDamage(5.0f);
        }
    }
}
