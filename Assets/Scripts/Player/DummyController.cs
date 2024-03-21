using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] float smoothingFactor;

 
    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, startPosition, smoothingFactor);     
    }

   
}


