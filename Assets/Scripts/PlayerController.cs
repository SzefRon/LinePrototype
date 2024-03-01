using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;

    [SerializeField] private Color color;

    private Rigidbody rb;
    private Renderer renderer;

    private float inputHorizontal;
    private float inputVertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<Renderer>();
        renderer.material.color = color;
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        inputVertical = Input.GetAxisRaw(inputNameVertical);
    }

    private void FixedUpdate()
    {
        Vector3 v = new (inputHorizontal * acceleration * Time.fixedDeltaTime, rb.velocity.y, inputVertical * acceleration * Time.fixedDeltaTime);
        v = v.normalized * maxSpeed;
        rb.AddForce(v, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
