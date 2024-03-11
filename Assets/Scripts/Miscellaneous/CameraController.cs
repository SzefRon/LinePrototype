using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

    [SerializeField] float smoothingFactor;

    Vector3 midpoint;

    // Update is called once per frame
    void Update()
    {
        midpoint = Vector3.Lerp(player1.position, player2.position, 0.5f);
        midpoint.y = transform.position.y;
        transform.position = Vector3.Lerp(transform.position, midpoint, smoothingFactor);
    }
}
