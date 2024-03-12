using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarComponent : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject UI;
    private HealthComponent healthComponent;
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        healthComponent = transform.parent.GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 healthBarPos = camera.WorldToScreenPoint(parent.transform.position);
    }
}
