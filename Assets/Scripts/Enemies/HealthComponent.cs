using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float Health
    {
        get { return health; }
    }
    private float health;
    private Renderer renderer;
    private Color startColor;
    private bool isBeingDamaged = false;

    void Start()
    {       
        health = maxHealth;
        renderer = GetComponent<Renderer>();
        startColor = renderer.material.color;
    }

    public float MaxHealthFraction(float fraction)
    {
        return maxHealth / fraction;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            Die();
        }
        else {
            StartCoroutine(FlashRed());
        }

    }

    IEnumerator FlashRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = startColor;
    }

    public void TakeDamageOverTime(float damage, float delay, int ticks)
    {
        if (!isBeingDamaged) {
            StartCoroutine(DamageOverTime(damage, delay, ticks));
        }
    }

    private IEnumerator DamageOverTime(float damage, float delay, int ticks)
    {
        isBeingDamaged = true;
        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(delay);
        }
        isBeingDamaged = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
