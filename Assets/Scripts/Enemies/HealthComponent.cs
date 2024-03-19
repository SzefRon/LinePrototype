using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float damageCooldownTime = 0.8f;
    public float Health
    {
        get { return health; }
    }
    private float health;
    public Renderer renderer;
    public Color startColor;
    public bool isMonster = false;
    private bool isDying = false;
    private Dictionary<SegmentUpgrades, bool> dotCooldown = new();
    private bool damageCooldown = false;

    [System.NonSerialized] public bool willMorph = false;

    void Awake()
    {       
        health = maxHealth;
        if (TryGetComponent<Renderer>(out renderer))
        {
            startColor = renderer.material.color;
        }
    }

    public float MaxHealthFraction(float fraction)
    {
        return maxHealth / fraction;
    }

    public void TakeDamage(float damage)
    {
        if (damageCooldown) return;
        StartCoroutine(DamageCooldown());
        ForceDamage(damage);
    }

    IEnumerator DamageCooldown()
    {
        damageCooldown = true;
        yield return new WaitForSeconds(0.5f);
        damageCooldown = false;
    }

    public void ForceDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
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
        if (!dotCooldown.ContainsKey(effect)) {
            dotCooldown.Add(effect, false);
        }
        if (!dotCooldown[effect]) {
            StartCoroutine(DamageOverTime(effect, damage, delay, ticks));
        }
    }

    private IEnumerator DamageOverTime(SegmentUpgrades effect, float damage, float delay, int ticks)
    {
        dotCooldown[effect] = true;
        for (int i = 0; i < ticks; i++)
        {
            ForceDamage(damage);
            yield return new WaitForSeconds(delay);
        }
        dotCooldown[effect] = false;
    }

    private void Die()
    {
        if (isDying) return;
        isDying = true;
<<<<<<< Updated upstream
        ChokeList.chokedObjects.Remove(gameObject);
        GetComponent<EnemyScript>().Drop();
        GetComponent<EnemyScript>().enabled = false;
        StartCoroutine(DieAnimation());
=======
        if (isMonster)
        {
            ChokeList.chokedObjects.Remove(gameObject);
            GetComponent<EnemyScript>().enabled = false;
            StartCoroutine(DieAnimation());
        }
        Destroy(gameObject);
>>>>>>> Stashed changes
    }

    private IEnumerator DieAnimation()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.position += Vector3.down * 0.05f;
            transform.localScale *= 0.95f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
