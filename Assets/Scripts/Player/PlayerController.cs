using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;

    [SerializeField] private KeyCode pullButton;

    [SerializeField] private KeyCode upButton;
    [SerializeField] private float pullCooldown = 1.0f;
    private bool isPulling = false;
    public int index;
    public Vector2 input;

    private Rigidbody rb;
    private Vector3 direciton;

    private ChokeManager chokeManager;
    private HealthComponent healthComponent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        chokeManager = FindAnyObjectByType<ChokeManager>();
        healthComponent = GetComponent<HealthComponent>();
    }

    public void MovementHandling(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().normalized;
    }

    void FixedUpdate()
    {
        //Debug.Log(input);
        Vector3 v = new(input.x * acceleration * Time.fixedDeltaTime, rb.velocity.y, input.y * acceleration * Time.fixedDeltaTime);
        
        v = v.normalized * maxSpeed;
        
        rb.AddForce(v, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        input = new Vector2(Input.GetAxisRaw(horizontalInput), Input.GetAxisRaw(verticalInput));
        
        if(input.sqrMagnitude > 0) 
        {
            direciton = input;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pullButton))
        {
            if (!isPulling)
            {
                Debug.Log("Pulling");
                rb.AddForce(direciton * 100.0f, ForceMode.Impulse);
                chokeManager.PullRope(index);
                StartCoroutine(PullCooldown());
            }
        }
    }

    private IEnumerator PullCooldown()
    {
        isPulling = true;
        yield return new WaitForSeconds(pullCooldown);
        isPulling = false;
    }
}
