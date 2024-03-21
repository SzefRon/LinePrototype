using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private UnityEngine.InputSystem.PlayerInput playerInput;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        var index = playerInput.playerIndex;
        var players = FindObjectsOfType<PlayerController>();
        foreach (var player in players)
        {
            if (player.index == index)
            {
                playerController = player;
                break;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerController.MovementHandling(context);
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerController.usingController = true;
            playerController.PullRope();
        }
    }
}
