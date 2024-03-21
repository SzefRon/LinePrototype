using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarLookat : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
