using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float damageCooldownTime = 0.8f;
    [SerializeField] public Renderer renderer;
    [SerializeField] public int morphUpgrades;
    [SerializeField] public int morphUpgradesToAddOneMorph = 2;
    public float Health
    {
        get { return health; }
    }
    public float health;
    public Color startColor;
    public bool isMonster = false;
    public bool isDying = false;
    public Dictionary<SegmentUpgrades, bool> dotCooldown = new();
    public bool damageCooldown = false;

    [System.NonSerialized] public bool willMorph = false;

    void Awake()
    {
        health = maxHealth;
        startColor = renderer.material.color;
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
        gameObject.GetComponent<PlayerController>().dummy.GetComponent<DummyController>().Dmg();
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
            if(gameObject.GetComponent<RescaleBounce>() != null) 
            {
                gameObject.GetComponent<RescaleBounce>().StartBounce();
            }
            StartCoroutine(Flash(Color.red));
        }
    }

    public void ForceHeal(float heal)
    {
        health += heal;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Flash(Color.green));
            gameObject.GetComponent<PlayerController>().dummy.GetComponent<DummyController>().Heal();
        }
    }

    IEnumerator FlashRed()
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = startColor;
    }

    IEnumerator Flash(Color color)
    {
        renderer.material.color = color;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = startColor;
    }

    public void TakeDamageOverTime(SegmentUpgrades effect, float damage, float delay, int ticks)
    {
        if (!dotCooldown.ContainsKey(effect))
        {
            dotCooldown.Add(effect, false);
        }
        if (!dotCooldown[effect])
        {
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
        if (isMonster)
        {
            if (willMorph)
            {
                for(int i = morphUpgrades / morphUpgradesToAddOneMorph; i > 0; i--)
                {
                    GetComponent<EnemyScript>().Morph();
                }
            }
            ChokeList.chokedObjects.Remove(gameObject);
            GetComponent<EnemyScript>().Drop();
            GetComponent<EnemyScript>().enabled = false;
            StartCoroutine(DieAnimation());
        }
        Destroy(gameObject);
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
