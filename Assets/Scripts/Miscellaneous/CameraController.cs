using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform player1;
    [SerializeField] public Transform player2;

    [SerializeField] float smoothingFactor;
    [SerializeField] float height;

    Vector3 midpoint;



    // Update is called once per frame
    void Update()
    {
        midpoint = Vector3.Lerp(player1.position, player2.position, 0.5f);
        midpoint.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, midpoint, smoothingFactor);
        transform.position += -1.0f * height * Vector3.forward;
        midpoint.y = 1.0f;
        transform.LookAt(midpoint);
    }
}
