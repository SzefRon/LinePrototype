using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour
{
    [SerializeField] GameObject s;
    public Slider slider;
    public HealthComponent healthComponent;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        slider = s.GetComponent<UnityEngine.UI.Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHP = healthComponent.health / healthComponent.maxHealth;
        slider.value = currentHP;
    }
}
