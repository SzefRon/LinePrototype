using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    Coroutine DOTcoroutine;
    public float Health
    {
        get { return health; }
    }
    private float health;

    void Start()
    {       
        health = maxHealth;
    }

    public float MaxHealthFraction(int fraction)
    {
        return maxHealth * fraction;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamageOverTime(float damage, float delay, int ticks)
    {
        if (DOTcoroutine != null)
        {
            StopCoroutine(DOTcoroutine);
        }
        DOTcoroutine = StartCoroutine(DamageOverTime(damage, delay, ticks));
    }

    private IEnumerator DamageOverTime(float damage, float delay, int ticks)
    {
        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(delay);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
