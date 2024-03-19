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
    private bool isDying = false;
    private readonly Dictionary<SegmentUpgrades, bool> dotCheck = new();

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

    public void TakeDamageOverTime(SegmentUpgrades effect, float damage, float delay, int ticks)
    {
        if (!dotCheck.ContainsKey(effect)) {
            dotCheck.Add(effect, false);
        }
        if (!dotCheck[effect]) {
            StartCoroutine(DamageOverTime(effect, damage, delay, ticks));
        }
    }

    private IEnumerator DamageOverTime(SegmentUpgrades effect, float damage, float delay, int ticks)
    {
        dotCheck[effect] = true;
        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(delay);
        }
        dotCheck[effect] = false;
    }

    private void Die()
    {
        if (isDying) return;
        isDying = true;
        ChokeList.chokedObjects.Remove(gameObject);
        GetComponent<EnemyScript>().enabled = false;
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.position += Vector3.down * 0.05f;
            transform.localScale *= 0.95f;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
}
