using System.Collections;
using UnityEngine;

public class MinionScript : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.1f;
    private HealthComponent healthComponent;
    Transform followTarget = null;
    [SerializeField] float minonDmg;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        StartCoroutine(SetTarget());
        //StartCoroutine(Lifetime());
    }

    IEnumerator SetTarget()
    {
        yield return new WaitForSeconds(1.0f);
        var enemies = GameObject.FindGameObjectsWithTag("Monster");
        var players = GameObject.FindGameObjectsWithTag("Player");
        
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
        if (followTarget == null)
        {
            foreach (var player in players)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance < 50.0f && distance < minDistance)
                {
                    minDistance = distance;
                    followTarget = player.transform;
                }
            }
        }

    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10.0f);
        healthComponent.TakeDamage(healthComponent.MaxHealthFraction(1.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget == null)
        {
            Debug.Log("No target!! WTF?");
            StartCoroutine(SetTarget());
        }
        Vector3 direction = (followTarget.position - transform.position).normalized;
        transform.position += direction * followSpeed;


    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Monster")
        {
            var otherHealthComponent = other.gameObject.GetComponent<HealthComponent>();
            otherHealthComponent.TakeDamage(minonDmg);
        }
    }
}
