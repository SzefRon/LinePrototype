using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    public int index;
    public Vector2 input;

    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MovementHandling(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().normalized;
    }

    void FixedUpdate()
    {
        Debug.Log(input);
        Vector3 v = new(input.x * acceleration * Time.fixedDeltaTime, rb.velocity.y, input.y * acceleration * Time.fixedDeltaTime);
        v = v.normalized * maxSpeed;
        rb.AddForce(v, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        input = new Vector2(Input.GetAxisRaw(horizontalInput), Input.GetAxisRaw(verticalInput));
    }
}
