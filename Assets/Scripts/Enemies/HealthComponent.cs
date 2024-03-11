using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float Health
    {
        get { return health; }
        set { health = value; }
    }
    private float health;

    void Start()
    {
        
    }}
