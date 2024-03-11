using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private string inputNameHorizontal;
    [SerializeField] private string inputNameVertical;

    private Rigidbody rb;

    private float inputHorizontal;
    private float inputVertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        inputHorizontal = Input.GetAxisRaw(inputNameHorizontal);
        inputVertical = Input.GetAxisRaw(inputNameVertical);
    }

    private void FixedUpdate()
    {
        Vector3 v = new(inputHorizontal * acceleration * Time.fixedDeltaTime, rb.velocity.y, inputVertical * acceleration * Time.fixedDeltaTime);
        v = v.normalized * maxSpeed;
        rb.AddForce(v, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
