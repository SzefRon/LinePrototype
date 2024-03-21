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
    private Vector3 direction;

    private ChokeManager chokeManager;
    private HealthComponent healthComponent;
    public bool usingController = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        chokeManager = FindAnyObjectByType<ChokeManager>();
        healthComponent = GetComponent<HealthComponent>();
    }

    public void MovementHandling(InputAction.CallbackContext context)
    {
        if (usingController)
        {
            input = context.ReadValue<Vector2>();
        }
    }

    void FixedUpdate()
    {
        Vector3 v = new(input.x * acceleration * Time.fixedDeltaTime, rb.velocity.y, input.y * acceleration * Time.fixedDeltaTime);
        
        v = v.normalized * maxSpeed;
        
        rb.AddForce(v, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (!usingController)
        {
            input = new Vector2(Input.GetAxisRaw(horizontalInput), Input.GetAxisRaw(verticalInput));
        }
        
        if(input.sqrMagnitude > 0) 
        {
            direction = input;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pullButton))
        {
            usingController = false;
            PullRope();
        }

        CheckIfAltarInRange();
    }

    public void PullRope()
    {
        if (!isPulling)
        {
            Debug.Log("Pulling");   
            rb.AddForce(direction * 100.0f, ForceMode.Impulse);
            chokeManager.PullRope(index);
            StartCoroutine(PullCooldown());
        }
        
    }

    private IEnumerator PullCooldown()
    {
        isPulling = true;
        yield return new WaitForSeconds(pullCooldown);
        isPulling = false;
    }
    public void CheckIfAltarInRange()
    {
        var altars = GameObject.FindGameObjectsWithTag("Altar");
        if (altars == null)
        {
            Debug.Log("Where are the altars??");
            return;
        }
        foreach (var altar in altars)
        {
            float distance = Vector3.Distance(altar.transform.position, transform.position);
            if (distance < 5.0f)
            {
                FindAnyObjectByType<RopeSelectionManager>().isInAltarRange = true;
                Debug.Log("In Altar Range");
                return;
            }
            else
            {
                FindAnyObjectByType<RopeSelectionManager>().isInAltarRange = false;
                return;
            }
        }
    }
}
