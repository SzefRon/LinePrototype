using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] float smoothingFactor;
    Color startColor;
    [SerializeField] Renderer renderer;
    private void Awake()
    {
        startPosition = transform.position;
        startColor = renderer.material.color;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, startPosition, smoothingFactor);     
    }

    IEnumerator Flash(Color color)
    {

        renderer.material.color = color;
        yield return new WaitForSeconds(0.2f);
        renderer.material.color = startColor;
    }

    public void Heal() 
    {
        StartCoroutine(Flash(Color.green));   
    }

    public void Dmg()
    {
        StartCoroutine(Flash(Color.red));
    }
}


