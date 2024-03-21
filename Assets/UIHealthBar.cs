using UnityEngine.UI;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    public HealthComponent health;
    public GameObject s;
    public Slider slider;

    void Start()
    {
        slider = s.GetComponent<UnityEngine.UI.Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHP = health.health / health.maxHealth;
        slider.value = currentHP;
    }
}
